using Business.IService.Customer;
using Microsoft.AspNetCore.Mvc;
using Util.Log;

namespace Demo.Web.Controllers
{
    public class HomeController : BaseController
    {
        public readonly ILoggerFactory _factory;
        public readonly IComsumeService _customer;
        public HomeController(ILoggerFactory factory, IComsumeService comsumeService)
        {
            _factory = factory;
            _customer = comsumeService;
        }
        public IActionResult Index()
        {
            _factory.Info("ces");
            throw new System.Exception("1231231");
            return View();
        }


    }
}
