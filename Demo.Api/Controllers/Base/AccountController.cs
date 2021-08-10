using Business.IService.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Util.Encrypt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Repository.Entity.AuthModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Demo.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService userService;
        private readonly IDepartmentService departmentService;
        private readonly JWTConfig jWTConfig;

        public AccountController(IUserService userService,
            IDepartmentService departmentService,
           IOptions<JWTConfig> jWTConfig)
        {
            this.userService = userService;
            this.departmentService = departmentService;
            this.jWTConfig = jWTConfig.Value;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet,AllowAnonymous]
        public async Task<IActionResult> Login(string account, string password)
        {
            UserEntity user = await userService.GetEntity(p => p.Account == account);
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

            JwtSecurityToken token = new JwtSecurityToken(issuer: jWTConfig.Issuer,
                                                          audience: jWTConfig.Audience,
                                                          claims: claims,
                                                          expires: DateTime.Now.AddMinutes(value: jWTConfig.AccessTokenExpiresMinutes),
                                                          //资格证书 秘钥加密
                                                          signingCredentials: new SigningCredentials(
                                                                                                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTConfig.IssuerSigningKey)),
                                                                                                     SecurityAlgorithms.HmacSha256));
            var tokenResult = string.Empty;
            try
            {
                tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {

              
                throw;
            }
            var tokeResult = new
            {
                token = "Bearer " + tokenResult,
                Expires_in = jWTConfig.AccessTokenExpiresMinutes * 60
            };
            return Ok(tokenResult);
        }

        [HttpPost]
        public IActionResult AuthTest()
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            return Ok(username);
        }
    }
}
