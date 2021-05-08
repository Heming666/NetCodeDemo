using Business.IService.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Extension;
using Util.Log;

namespace Demo.Web.Areas.Base.Controllers
{
    [Area("Base")]
    public class DeptController : Controller
    {
        public ILoggerFactory _log { get; }
        public IDepartmentService _deptService { get; }

        public DeptController(ILoggerFactory factory, IDepartmentService departmentService)
        {
            _log = factory;
            _deptService = departmentService;
        }
        // GET: DeptController
        public ActionResult Index()
        {
            List<DepartmentEntity> depts = _deptService.GetList(ExpressionExtension.True<DepartmentEntity>());
            
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
        public ActionResult Create(DepartmentEntity entity)
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
