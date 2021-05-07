using Business.IService.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Log;

namespace Demo.Web.Controllers
{
    public class HomeController : BaseController
    {
        public readonly ILoggerFactory _factory;
        public readonly IUserService _userService;
        public HomeController(ILoggerFactory factory, IUserService userService)
        {
            _factory = factory;
            _userService = userService;
        }
        public IActionResult Index()
        {
            _factory.Info("我的日志");
            return View();
        }
    }
}
