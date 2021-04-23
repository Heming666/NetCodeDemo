using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Controllers
{
    public class BaseController : Controller
    {

        public readonly Logger logger;
        public BaseController()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
    }
}
