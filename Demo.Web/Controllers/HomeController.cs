using Business.IService.Customer;
using Microsoft.AspNetCore.Mvc;
using Util.Log;

namespace Demo.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IComsumeService _customer;

        public HomeController( IComsumeService comsumeService,ILoggerFactory logger) :base(logger)
        {
            _customer = comsumeService;
        }
        public IActionResult Index()
        {
            logger.Info("ces");
            return View();
        }


    }
}
