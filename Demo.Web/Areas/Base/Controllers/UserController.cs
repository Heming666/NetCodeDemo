using Business.IService.Base;
using Microsoft.AspNetCore.Authorization;
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
    public class UserController : Controller
    {
        private readonly ILoggerFactory _logFactory;
        private readonly IUserService _userService;
        private readonly IDepartmentService _deptService;
        public UserController(ILoggerFactory factory,
                              IUserService userService,
                              IDepartmentService departmentService)
        {
            this._logFactory = factory;
            this._userService = userService;
            this._deptService = departmentService;
        }
        //[Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var users = _userService.GetList(ExpressionExtension.True<UserEntity>());
            return View(users);
        }
        // GET: DeptController/Create
        public ActionResult Create()
        {
            List<DepartmentEntity> depts = _deptService.GetList(ExpressionExtension.True<DepartmentEntity>());
            ViewBag.Depts = depts;
            return View();
        }

        // POST: DeptController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserEntity entity)
        {
            try
            {
                _userService.Insert(entity);
                return RedirectToAction(nameof(Index));
            }
            catch( Exception ex)
            {
                _logFactory.Error(ex);
                return View();
            }
        }

    }
}
