using Business.IService.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Extension;
using Util.Log;

namespace Demo.Web.Areas.Base.Controllers
{

    [Area("Base")]
    public class UserController : Controller
    {
        private readonly ILoggerFactory _logFactory;
        private readonly IUserService _userService;
        public UserController(ILoggerFactory factory, IUserService userService)
        {
            this._logFactory = factory;
            this._userService = userService;
        }
        //[Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            List<UserEntity> depts = _userService.GetList(ExpressionExtension.True<UserEntity>());
            return View();
        }
    }
}
