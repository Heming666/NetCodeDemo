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
    /* Configure �� ConfigureServices ֧�ִ��� Configure<EnvironmentName> �� Configure<EnvironmentName>Services �Ļ����ض��汾�� 
     * ��Ӧ����ҪΪ���������ÿ����������������죩�������ʱ�����ַ����ͷǳ����ã�
     **/
    public class Startup
    {
        /// <summary>
        /// �����ļ�
        /// </summary>
        public IConfiguration Configuration { get; set; }
        /// <summary>
        /// ��������
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
            //ע��mysqlDbcontext
            services.AddDbContext<MySqlDBContext>(option =>
            {
                option.UseMySql(
                    Configuration.GetConnectionString("mysql"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("mysql")),
                    option =>
                    {
                    });
            });
            ////ע��oracleDbcontext
            //services.AddDbContext<OracleDBContext>(option =>
            //{
            //    option.UseOracle(
            //        Configuration.GetConnectionString("oracle"),
            //        option =>
            //        {
            //            //��������
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
            services.AddDataProtection();//����Windows DPAPI �Զ�����  --���ڶԳƼ���
            services.AddResponseCompression();
            services.AddMvc().AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/Login/Login");
                }).AddRazorRuntimeCompilation();
            services.AddSession();
            services.AddMemoryCache();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    var cookiePolicyOptions = new CookiePolicyOptions
                    {

                        //MinimumSameSitePolicy = SameSiteMode.Lax,//OAuth2验证需允许跨越站点
                        MinimumSameSitePolicy = SameSiteMode.Strict,
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
             policy => policy.Requirements.Add(new PermissionRequirement(
                "/Home/visitDeny",// 验证未通过后跳转的地址
                ClaimTypes.Name,//验证的策略的属性
                expiration: TimeSpan.FromSeconds(60 * 5)//身份过期时间
                )));
            });
            // ע��Ȩ�޴�����
            services.AddTransient<IAuthorizationHandler, OwnerAuthorizationHandler>();
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";//��Ȩ�޷��ʺ� ��ת�ĵ�ַ
                options.Cookie.Name = "YourAppCookieName";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.LoginPath = "/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });


            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddDirectoryBrowser();//���Ŀ¼���


            services.AddScoped<IRepositoryFactory, RepositoryFactory>();//ע�����ݹ���
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
                //�ǿ������������¼��־ ��������http����
                app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            }
            //��ֹ��վ�㹥��XSRF/CSRF
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
            app.UseResponseCompression();//��Ӧѹ���� ����ע���м��services.AddResponseCompression();
            app.UseResponseCaching();
            app.UseRouting();
            app.UseHttpsRedirection();//HTTPS��ת
            app.UseStaticFiles();//��̬�ļ�
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Resource")),
                RequestPath = new PathString("/src")
            });
            //������ֱ���������Ԥ�����ļ�����·��
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
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                        );
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
