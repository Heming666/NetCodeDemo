using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Log;

namespace Demo.Web.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 日志工程
        /// </summary>
        public readonly ILoggerFactory logger;
        public BaseController(ILoggerFactory loggerFactory) 
        {
            logger = loggerFactory;
            var b = RouteData;

            string controller = RouteData.Values["controller"].ToString();
            logger.Setting(this.HttpContext.Request.RouteValues["controller"].ToString());
        }
    }
}
