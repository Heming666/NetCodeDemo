using Business.IService.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Util.Encrypt;
using Util.Extension;
using Util.Log;

namespace Demo.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDepartmentService _deptService;
        private readonly IWebHostEnvironment _hosting;
        /// <summary>
        /// 数据加密
        /// </summary>
        private readonly IDataProtector _protector;
        public AccountController(IUserService userService, IDepartmentService deptService, IDataProtectionProvider protector, IWebHostEnvironment hosting)
        {
            string key = "PublicKey";//key为加密公钥，私钥为系统自动维护    
            _protector = protector.CreateProtector(key);
            _userService = userService;
            _deptService = deptService;
            _hosting = hosting;
        }
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            string username = string.Empty, password = string.Empty;
            var cookieValue = HttpContext.Request.Cookies["User_Account"];
            if (!cookieValue.IsEmpty())
            {
                try
                {
                    dynamic UserInfo = JsonConvert.DeserializeAnonymousType(_protector.Unprotect(cookieValue), new { Account = string.Empty, PassWord = string.Empty });
                    username = UserInfo.Account;
                    password = UserInfo.PassWord;
                    //此处可以不经过登录，直接进入平台
                }
                catch (Exception)
                {
                    //大概率是DPAPI 私钥过期  
                }

            }
            ViewBag.Account = username;
            ViewBag.PassWord = password;
            return View();
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <returns></returns>
        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(IFormCollection forms)
        {
            try
            {
                string account = forms["Account"], password = forms["PassWord"], isRemember = forms["IsRememberMe"];
                UserEntity user = await _userService.GetEntity(p => p.Account == account);
                if (user is null) throw new Exception("账号或密码错误");
                if (!(user.PassWord.Equals(password) || user.PassWord.Equals(MD5Encrypt.MD5Encrypt16(password)))) throw new Exception("账号或密码错误");


                //记住密码
                var claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Name, user.UserName),
                                        new Claim(ClaimTypes.WindowsAccountName,user.Account ),
                                        new Claim(ClaimTypes.Role, user.DeptInfo.DeptName),
                                        new Claim(ClaimTypes.PrimarySid,user.ID.ToString()),
                                        new Claim(ClaimTypes.GroupSid,user.DeptId.ToString()),
                                           new Claim(ClaimTypes.WindowsDeviceGroup,user.DeptInfo.DeptName)
                                    };
               
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.
                    IsPersistent = true,

                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),

                    // Refreshing the authentication session should be allowed.
                    AllowRefresh = true,




                    // The time at which the authentication ticket was issued.
                    //IssuedUtc = <DateTimeOffset>,


                    RedirectUri = Url.Action(nameof(Login))   // The full path or absolute URI to be used as an http  // redirect response value.
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                if (!isRemember.IsEmpty() && isRemember.Equals("remember-me"))
                {
                    //采用对称加密，对用户信息进行加密.。写入cookie
                    string cookieValue = JsonConvert.SerializeObject(new { user.Account, user.PassWord });
                    string encyptString = _protector.Protect(cookieValue);
                    HttpContext.Response.Cookies.Append("User_Account", encyptString, new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddDays(30) });
                }
             
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return View();
        }


        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Register()
        {
            UserEntity entity = new UserEntity() { CreateDate = DateTime.Now };
            var depts = await _deptService.GetList(Util.Extension.ExpressionExtension.True<DepartmentEntity>());
            ViewBag.Depts = depts;
            return View(entity);
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(UserEntity user)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files["Photo"];
                if (file != null)
                {
                    string newfileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string temporary = Path.Combine(_hosting.WebRootPath, "Resource/Photo");//临时保存分块的目录
                    if (!Directory.Exists(temporary))
                        Directory.CreateDirectory(temporary);
                    string filePath = Path.Combine(temporary, newfileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        await file.CopyToAsync(fs);
                    }
                    user.Photo = "src/Photo/" + newfileName;
                }
                user.PassWord = MD5Encrypt.MD5Encrypt16(user.PassWord);
                bool isOK = await _userService.Register(user) > 0;
                return RedirectToAction(nameof(Login));
            }
            else
            {
                var depts = await _deptService.GetList(Util.Extension.ExpressionExtension.True<DepartmentEntity>());
                ViewBag.Depts = depts;
                user.CreateDate = DateTime.Now;
                return View(user);
            }
        }

        /// <summary>
        /// 验证账号是否存在
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyAccount(string account)
        {
            if (await _userService.ExistsAccount(account))
            {
                return Json($"账号已存在");
            }
            return Json(true);
        }
    }
}
