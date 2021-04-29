using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Areas.Base.Controllers
{
    public class UserController : Controller
    {
        //[Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
