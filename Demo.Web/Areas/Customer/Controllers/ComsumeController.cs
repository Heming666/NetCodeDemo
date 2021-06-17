using Business.IService.Customer;
using Demo.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.Models.Consume;
using Repository.Entity.ViewModels.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Util.Extension;
using Util.Log;

namespace Demo.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ComsumeController : BaseController
    {
        private readonly NLog.Logger logger;
        private readonly IComsumeService _customer;

        public ComsumeController(IComsumeService comsumeService)
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
            this._customer = comsumeService;
        }
     /// <summary>
     /// 消费首页
     /// </summary>
     /// <param name="classify"></param>
     /// <param name="UserId"></param>
     /// <returns></returns>
        public async Task<IActionResult> Index(int? classify=null)
        {
        
            var where = ExpressionExtension.True<ConsumeEntity>().And(x => x.UserId == CurrentUser().UserId);
            if (classify.HasValue) where = where.And(x => x.Classify == (Classify)classify.Value);
            var datas =await _customer.GetListAsync(where);
            return View(datas);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="classify"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Index"),HttpPost,ValidateAntiForgeryToken]
        public async  Task<IActionResult> IndexPost(int? classify = null)
        {
            var where = ExpressionExtension.True<ConsumeEntity>().And(x => x.UserId == CurrentUser().UserId);
            if (classify.HasValue) where = where.And(x => x.Classify == (Classify)classify.Value);
            var datas =await _customer.GetListAsync(where);
            return View(nameof(Index),datas);
        }

        // GET: ComsumeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ComsumeController/Create
        public ActionResult Create()
        {
            return View(new ConsumeEntity() { LogTime=DateTime.Now,CreateTime=DateTime.Now });
        }

        // POST: ComsumeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConsumeEntity entity)
        {
            try
            {
                entity.UserId = CurrentUser().UserId;
                entity.CreateTime = DateTime.Now;
                var success = _customer.Add(entity).Result > 0;
                if (success)
                {
                    return RedirectToAction(nameof(Index), new { UserId = CurrentUser().UserId });
                }
                else
                {
                    ModelState.AddModelError("Error", "添加失败");
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                ModelState.AddModelError("Error", ex.Message);
            }
            return View(entity);
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
            List<ChartsPieModel> data = await _customer.LoadPie(x => x.LogTime.Month == month && x.UserId.Equals(CurrentUser().UserId));
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> LoadYearPie()
        {
            int year = DateTime.Now.Year;
            List<ChartsPieModel> data = await _customer.LoadPie(x => x.LogTime.Year == year && x.UserId.Equals(CurrentUser().UserId));
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> LoadColumn(int year)
        {
            List<ChartsColumnModel> data = await _customer.LoadColumn(x => x.LogTime.Year == year && x.UserId.Equals(CurrentUser().UserId));
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> LoadMonthColumn(int month)
        {
            List<ChartsColumnModel> data = await _customer.LoadMonthColumn(x => x.LogTime.Month == month && x.UserId.Equals(CurrentUser().UserId));
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
