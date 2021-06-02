using Business.IService.Base;
using Demo.Web.Controllers;
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
        private readonly NLog.Logger logger;
        private readonly IUserService _userService;
        private readonly IDepartmentService _deptService;
        public UserController(IUserService userService,
                              IDepartmentService departmentService)
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
            this._userService = userService;
            this._deptService = departmentService;
        }
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetList(ExpressionExtension.True<UserEntity>());
            return View(users);
        }
        // GET: DeptController/Create
        public async Task<IActionResult> Create()
        {
            var depts = await _deptService.GetList(ExpressionExtension.True<DepartmentEntity>());
            ViewBag.Depts = depts;
            return View();
        }

        // POST: DeptController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserEntity entity)
        {
            try
            {
                await _userService.Insert(entity);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return View();
            }
        }

    }
}
