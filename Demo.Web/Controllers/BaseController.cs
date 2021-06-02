using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Log;

namespace Demo.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public BaseController()
        {
           // NLog.Logger a = NLog.LogManager.GetCurrentClassLogger();
        }
    }
}
