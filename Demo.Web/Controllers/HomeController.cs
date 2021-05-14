using Business.IService.Base;
using Business.IService.Customer;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.ViewModels.Index;
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
        public readonly IComsumeService _customer;
        public HomeController(ILoggerFactory factory, IComsumeService comsumeService)
        {
            _factory = factory;
            _customer = comsumeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadPie(int month)
        {
            List<ChartsPieModel> data = await _customer.LoadPie(x => x.LogTime.Month == month && x.UserId.Equals(1));
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> LoadYearPie()
        {
            int year = DateTime.Now.Year;
            List<ChartsPieModel> data = await _customer.LoadPie(x => x.LogTime.Year == year && x.UserId.Equals(1));
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> LoadColumn(int year)
        {
            List<ChartsColumnModel> data = await _customer.LoadColumn(x => x.LogTime.Year==year && x.UserId.Equals(1));
            return Json(data);
        }
    }
}
