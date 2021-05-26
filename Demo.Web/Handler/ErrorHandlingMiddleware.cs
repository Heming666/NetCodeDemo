﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Log;

namespace Demo.Web.Handler
{
    /// <summary>
    /// 全局异常捕获
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly ILoggerFactory _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            this._next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var statusCode = context.Response.StatusCode;
                if (e is Exception)
                {
                    statusCode = 500;
                }

                await HandleExceptionAsync(context, statusCode, e);
            }
        }

        private  Task HandleExceptionAsync(HttpContext context, int statusCode, Exception e)
        {
            if (statusCode != 200)
            {
                _logger.Error(context.Request.Host.Value + context.Request.Path.Value + "\r\n" + JsonConvert.SerializeObject(e));
            }
            var data = new { code = statusCode.ToString(), is_success = false, msg = e.Message };
            var result = JsonConvert.SerializeObject(new { data = data });
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }
}
