using Business.IService.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public LoginController(ILoggerFactory factory,IUserService userService)
        {
            _factory = factory;
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Regist()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

    }
}
