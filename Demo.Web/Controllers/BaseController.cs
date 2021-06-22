using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Util.Extension;
using Util.Log;

namespace Demo.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public UserInfoModel UserInfo { get; private set; }
        public BaseController()
        {
           // NLog.Logger a = NLog.LogManager.GetCurrentClassLogger();
        }
        /// <summary>
        /// 获取当前用户登录信息
        /// </summary>
        /// <returns></returns>
        public UserInfoModel CurrentUser()
        {
            this.UserInfo = UserInfo.GetUserInfo(this);
            return this.UserInfo;
        }


        /// <summary>
        /// 登录用户的信息
        /// </summary>

    }
    public struct UserInfoModel
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string Account { get; private set; }
        public string Role { get; private set; }
        public string DeptId { get; private set; }
        public string DeptName { get; private set; }
        public UserInfoModel GetUserInfo(ControllerBase controllerBase)
        {
            this.UserId = controllerBase.User.FindFirst(ClaimTypes.PrimarySid).Value.ToInt();
            this.UserName = controllerBase.User.FindFirst(ClaimTypes.Name).Value;
            this.Account = controllerBase.User.FindFirst(ClaimTypes.WindowsAccountName).Value;
            this.Role = controllerBase.User.FindFirst(ClaimTypes.Role).Value;
            this.DeptId = controllerBase.User.FindFirst(ClaimTypes.GroupSid).Value;
            this.DeptName = controllerBase.User.FindFirst(ClaimTypes.WindowsDeviceGroup).Value;
            return this;
        }
    }
}
