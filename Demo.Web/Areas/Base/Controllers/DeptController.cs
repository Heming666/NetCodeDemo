using Business.IService.Base;
using Demo.Web.Common.Caches;
using Demo.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Cache;
using Util.Extension;
using Util.Log;

namespace Demo.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class DeptController : BaseController
    {
        private readonly ICache cache;
        private readonly DeptCache deptCache;

        private IDepartmentService _deptService { get; }

        public DeptController(
            IDepartmentService departmentService,
            ICache cache)
        {
            _deptService = departmentService;
            this.cache = cache;
            deptCache = new DeptCache(departmentService, cache);
        }
        // GET: DeptController
        public async Task<IActionResult> Index()
{
            var depts = await deptCache.LoadDepts();
            return View(depts);
        }

        // GET: DeptController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeptController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeptController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentEntity entity)
        {
            try
            {
               await _deptService.Insert(entity);
                await deptCache.RefreshDepts();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DeptController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeptController/Edit/5
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

        // GET: DeptController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeptController/Delete/5
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
