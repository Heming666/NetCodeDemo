using Business.IService.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.Models.Consume;
using Repository.Entity.ViewModels.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Extension;
using Util.Log;

namespace Demo.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ComsumeController : Controller
    {
        private readonly ILoggerFactory _logger;
        private readonly IComsumeService _customer;

        public ComsumeController(ILoggerFactory logger,IComsumeService comsumeService)
        {
            this._logger = logger;
            this._customer = comsumeService;
        }
     /// <summary>
     /// 消费首页
     /// </summary>
     /// <param name="classify"></param>
     /// <param name="UserId"></param>
     /// <returns></returns>
        public ActionResult Index(int? classify=null, int UserId=1)
        {
            var where = ExpressionExtension.True<ConsumeEntity>().And(x => x.UserId == UserId);
            if (classify.HasValue) where = where.And(x => x.Classify == (Classify)classify.Value);
            var datas = _customer.GetListAsync(where);
            ViewBag.UserId = UserId;
            return View(datas);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="classify"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Index"),HttpPost,ValidateAntiForgeryToken]
        public ActionResult IndexPost(int? classify = null, int UserId = 1)
        {
            var where = ExpressionExtension.True<ConsumeEntity>().And(x => x.UserId == UserId);
            if (classify.HasValue) where = where.And(x => x.Classify == (Classify)classify.Value);
            var datas = _customer.GetListAsync(where);
            ViewBag.UserId = UserId;
            return View(nameof(Index),datas);
        }

        // GET: ComsumeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ComsumeController/Create
        public ActionResult Create(int UserId)
        {
            return View(new ConsumeEntity() { UserId = UserId,LogTime=DateTime.Now,CreateTime=DateTime.Now });
        }

        // POST: ComsumeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConsumeEntity entity)
        {
            try
            {
                _customer.Add(entity);
                return RedirectToAction(nameof(Index), new { UserId = entity.UserId });
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                return View();
            }
        }

        // GET: ComsumeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComsumeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ComsumeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ComsumeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #region 统计
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
            List<ChartsColumnModel> data = await _customer.LoadColumn(x => x.LogTime.Year == year && x.UserId.Equals(1));
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> LoadMonthColumn(int month)
        {
            List<ChartsColumnModel> data = await _customer.LoadMonthColumn(x => x.LogTime.Month == month && x.UserId.Equals(1));
            List<int> days = new List<int>();
            int day = System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            while (day >=1)
            {
                days.Add(day);
                day--;
            }
            var res = new { days = days.OrderBy(x=>x),  data };
            return Json(res);
        }
        #endregion
    }
}
