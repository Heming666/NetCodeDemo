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
        public HomeController( )
        {
      
        }
        public IActionResult Index()
        {
            logger.Info("测试");
            logger.Error("错误");
            return View();
        }
    }
}
