﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Log;

namespace Demo.Web.Controllers
{
    public class HomeController : BaseController
    {
        public readonly LoggerFactory _factory;
        public HomeController(LoggerFactory factory)
        {
            _factory = factory;
        }
        public IActionResult Index()
        {

            _factory.Info("我的日志");
            return View();
        }
    }
}
