using Business.IService.Base;
using Business.Service.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.EF;
using Repository.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Log;
using Newtonsoft.Json;
using System.Net;
using Business.Service.Customer;
using Business.IService.Customer;

namespace Demo.Web
{
    /* Configure 和 ConfigureServices 支持窗体 Configure<EnvironmentName> 和 Configure<EnvironmentName>Services 的环境特定版本。 
     * 当应用需要为多个环境（每个环境有许多代码差异）配置启动时，这种方法就非常有用：
     **/
    public class Startup
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public IConfiguration Configuration { get; set; }
        /// <summary>
        /// 环境变量
        /// </summary>
        public IWebHostEnvironment Env { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Util.Log.ILoggerFactory>(x =>
            {
                return new NLogService("XmlConfig/NLog.config");
            });
            services.AddResponseCompression();
            services.AddControllersWithViews();
            services.AddSession();
            services.AddMemoryCache();
            services.Configure<IISServerOptions>(options =>
           {
               options.AllowSynchronousIO = true;
           });
            services.AddScoped<DbContext, MySqlDBContext>();
            //注入mysqlDbcontext
            services.AddDbContext<MySqlDBContext>(option =>
            {
                option.UseMySql(
                    Configuration.GetConnectionString("mysql"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("mysql")),
                    option =>
                    {
                    });
            });
            ////注入oracleDbcontext
            //services.AddDbContext<OracleDBContext>(option =>
            //{
            //    option.UseOracle(
            //        Configuration.GetConnectionString("oracle"),
            //        option =>
            //        {
            //            //数据设置
            //        });
            //});
            //services.AddDbContext<MSSQLDBContext>(option =>
            //{
            //    option.UseSqlServer(
            //        Configuration.GetConnectionString("mssql"),
            //        option =>
            //        {

            //        });
            //});
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IComsumeService, ComsumeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Util.Log.ILoggerFactory loggerFactory)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
     
            app.UseCookiePolicy();
            app.UseRequestLocalization();
            app.UseCors();
            app.UseAuthentication();
            app.UseSession();
            app.UseResponseCompression();//响应压缩， 必须注册中间件services.AddResponseCompression();
            app.UseResponseCaching();

            app.UseRouting();
            app.UseHttpsRedirection();//HTTPS跳转
            app.UseStaticFiles();//静态文件
            app.UseAuthorization();
            //app.UseExceptionHandler(option => option.Run(async context =>
            //{
            //    await Task.Run(() =>
            //    {
            //        Exception ex = context.Features.Get<IExceptionHandlerFeature>().Error;
            //        loggerFactory.Error(ex, "全局错误");
            //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        context.Response.ContentType = "application/json";
            //        if (ex != null)
            //        {
            //            var errObj = JsonConvert.SerializeObject(ex);
            //            context.Response.WriteAsync(errObj);
            //        }
            //    });
            //})
            //);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                        );
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
