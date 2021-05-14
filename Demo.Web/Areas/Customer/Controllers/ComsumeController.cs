using Business.IService.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.Models.Consume;
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
        private readonly IComsumeService _comsumeService;

        public ComsumeController(ILoggerFactory logger,IComsumeService comsumeService)
        {
            this._logger = logger;
            this._comsumeService = comsumeService;
        }
        // GET: ComsumeController
        public ActionResult Index(int UserId)
        {
            List<ConsumeEntity> datas = _comsumeService.GetList(p=>p.UserId ==UserId);
            ViewBag.UserId = UserId;
            return View(datas);
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
                _comsumeService.Add(entity);
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
    }
}
