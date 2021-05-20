using Business.IService.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Controllers
{
    public class LoginController : Controller
    {
        public readonly ILoggerFactory _factory;
        public readonly IUserService _userService;
        public readonly IDepartmentService _deptService;
        public LoginController(ILoggerFactory factory,IUserService userService,IDepartmentService deptService)
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

    }
}
