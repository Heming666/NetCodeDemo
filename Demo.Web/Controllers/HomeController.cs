using Business.IService.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Util.Log;

namespace Demo.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly NLog.Logger logger;
        private readonly IComsumeService _customer;

        public HomeController( IComsumeService comsumeService)
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
            _customer = comsumeService;
        }
        [Authorize(Roles = "系统管理员, 管理人员")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
