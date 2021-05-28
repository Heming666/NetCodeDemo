using Business.IService.Base;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Util.Encrypt;
using Util.Log;

namespace Demo.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IDepartmentService _deptService;
        /// <summary>
        /// 数据加密
        /// </summary>
        private readonly IDataProtector _protector;
        public LoginController(ILoggerFactory factory, IUserService userService, IDepartmentService deptService, IDataProtectionProvider protector) : base(factory)
        {
            string key = "PublicKey";//key为加密公钥，私钥为系统自动维护  
            _protector = protector.CreateProtector(key);
            _userService = userService;
            _deptService = deptService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }


        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            UserEntity entity = new UserEntity() { CreateDate = DateTime.Now };
            var depts = _deptService.GetList(Util.Extension.ExpressionExtension.True<DepartmentEntity>());
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
                    string temporary = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource/Photo");//临时保存分块的目录
                    if (!Directory.Exists(temporary))
                        Directory.CreateDirectory(temporary);
                    string filePath = Path.Combine(temporary, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        await file.CopyToAsync(fs);
                    }
                }
                var a = _protector.Protect("1");
                Console.WriteLine(a);
                var b = _protector.Unprotect(a);
                Console.WriteLine(b);
                user.PassWord = MD5Encrypt.MD5Encrypt16(user.PassWord);
                bool isOK = await _userService.Register(user) >0;
                return RedirectToAction(nameof(Login));
            }
            else
            {
                var depts = _deptService.GetList(Util.Extension.ExpressionExtension.True<DepartmentEntity>());
                ViewBag.Depts = depts;
                user.CreateDate = DateTime.Now;
                return View(user);
            }
        }
    }
}
