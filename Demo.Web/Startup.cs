using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Log;

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
        public IConfiguration _configuration { get; set; }
        /// <summary>
        /// 环境变量
        /// </summary>
        public IWebHostEnvironment _env { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddControllersWithViews();
            services.AddSession();
            services.Configure<IISServerOptions>(options =>
           {
               options.AllowSynchronousIO = true;
           });

            services.AddSingleton<Util.Log.LoggerFactory>(x =>
            {
                return new NLogService("XmlConfig/NLog.config");
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();//HTTPS跳转
            app.UseStaticFiles();//静态文件
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseRequestLocalization();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseResponseCompression();//响应压缩， 必须注册中间件services.AddResponseCompression();
            app.UseResponseCaching();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                                                                    name: "MyArea",
                                                                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
