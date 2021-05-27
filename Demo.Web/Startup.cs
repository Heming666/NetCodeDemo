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

namespace Demo.Web
{
    /* Configure �� ConfigureServices ֧�ִ��� Configure<EnvironmentName> �� Configure<EnvironmentName>Services �Ļ����ض��汾�� 
     * ��Ӧ����ҪΪ���������ÿ����������������죩��������ʱ�����ַ����ͷǳ����ã�
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
            services.AddSingleton<Util.Log.ILoggerFactory>(x =>
            {
                return new NLogService("XmlConfig/NLog.config");
            });
            services.AddDataProtection();//����Windows DPAPI �Զ�����
            services.AddResponseCompression();
            services.AddControllersWithViews();
            services.AddSession();
            services.AddMemoryCache();
            services.Configure<IISServerOptions>(options =>
           {
               options.AllowSynchronousIO = true;
           });
            services.AddDirectoryBrowser();//����Ŀ¼���

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
            app.Use(next => context =>
            {
                string path = context.Request.Path.Value;
                if (
                    string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(path, "/login/login", StringComparison.OrdinalIgnoreCase))
                {
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false });
                }
                return next(context);
            });

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
          Path.Combine(Directory.GetCurrentDirectory(), @"Resource")),
                RequestPath = new PathString("/Resource")
            });
            //������Ԥ�����ļ�����·��
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot")),
                RequestPath=new PathString("/MyFile")//URL http://localhost:xxxx/images
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
