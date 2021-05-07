using Business.IService.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Log;

namespace Demo.Web.Areas.Base.Controllers
{
    public class UserController : Controller
    {
        private readonly ILoggerFactory _logFactory;
        private readonly IUserService _userService;
        public UserController(ILoggerFactory factory, IUserService userService)
        {
            this._logFactory = factory;
            this._userService = userService;
        }

        public IUserService UserService { get; }

        //[Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
