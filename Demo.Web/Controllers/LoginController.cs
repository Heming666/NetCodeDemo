using Business.IService.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Controllers
{
    public class LoginController : Controller
    {
        public readonly ILoggerFactory _factory;
        public readonly IUserService _userService;
        public readonly IDepartmentService _deptService;
        public LoginController(ILoggerFactory factory, IUserService userService, IDepartmentService deptService)
        {
            _factory = factory;
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
            List<DepartmentEntity> depts = _deptService.GetList(Util.Extension.ExpressionExtension.True<DepartmentEntity>());
            ViewBag.Depts = depts;
            return View(entity);
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(UserEntity user)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files["Photo"];
                string temporary = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photo");//临时保存分块的目录
                if (!Directory.Exists(temporary))
                    Directory.CreateDirectory(temporary);
                string filePath = Path.Combine(temporary, Guid.NewGuid().ToString()+Path.GetExtension(file.FileName));
                if (!Convert.IsDBNull(file))
                {

                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        await file.CopyToAsync(fs);
                    }
                }
                bool isOK = await _userService.Register(user);
                return RedirectToAction(nameof(Login));
            }
            else
            {
                List<DepartmentEntity> depts = _deptService.GetList(Util.Extension.ExpressionExtension.True<DepartmentEntity>());
                ViewBag.Depts = depts;
                user.CreateDate = DateTime.Now;
                return View(user);
            }
        }
    }
}
