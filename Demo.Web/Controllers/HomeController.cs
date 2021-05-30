using Business.IService.Customer;
using Microsoft.AspNetCore.Mvc;
using Util.Log;

namespace Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly NLog.Logger logger;
        private readonly IComsumeService _customer;

        public HomeController( IComsumeService comsumeService)
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
            _customer = comsumeService;
        }
        public IActionResult Index()
        {
            logger.Info(111);
            return View();
        }
    }
}
