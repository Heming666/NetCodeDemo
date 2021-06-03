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
using Demo.Web.Handler;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
            services.AddDataProtection();//开启Windows DPAPI 自动加密  --用于对称加密
            services.AddResponseCompression();
            services.AddControllersWithViews().AddRazorPagesOptions(options =>
        {
            options.Conventions.AuthorizePage("/Login/Login");
        });
            services.AddSession();
            services.AddMemoryCache();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    var cookiePolicyOptions = new CookiePolicyOptions
                    {
                        //默认 MinimumSameSitePolicy 值为 SameSiteMode.Lax 允许 OAuth2 authentication。 若要严格地强制执行同一站点策略 设置 MinimumSameSitePolic=SameSiteMode.Strict
                        MinimumSameSitePolicy = SameSiteMode.Strict,
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
             policy => policy.Requirements.Add(new PermissionRequirement(
                "/Home/visitDeny",// 拒绝授权的跳转地址
                ClaimTypes.Name,//基于用户名的授权
                expiration: TimeSpan.FromSeconds(60 * 5)//接口的过期时间
                )));
            });
            // 注入权限处理器
            services.AddTransient<IAuthorizationHandler, OwnerAuthorizationHandler>();
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";//无权限访问后 跳转的地址
                options.Cookie.Name = "YourAppCookieName";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.LoginPath =  "/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });


            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddDirectoryBrowser();//启动目录浏览


            services.AddScoped<IRepositoryFactory, RepositoryFactory>();//注册数据工厂
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IComsumeService, ComsumeService>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IAntiforgery antiforgery)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //非开发环境，则记录日志 ，并处理http请求
                app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            }
            //阻止跨站点攻击XSRF/CSRF
            //app.Use(next => context =>
            //{
            //    string path = context.Request.Path.Value;
            //    if (
            //        string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
            //        string.Equals(path, "/login/login", StringComparison.OrdinalIgnoreCase))
            //    {
            //        var tokens = antiforgery.GetAndStoreTokens(context);
            //        context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
            //            new CookieOptions() { HttpOnly = false });
            //    }
            //    return next(context);
            //});
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
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Resource")),
                RequestPath = new PathString("/src")
            });
            //设置能直接在浏览器预览的文件夹与路径
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files")//URL http://localhost:xxxx/Files
            });
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}"
                        );
                endpoints.MapControllerRoute("default", "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
