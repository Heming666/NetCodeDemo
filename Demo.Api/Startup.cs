using Business.IService.Base;
using Business.IService.Customer;
using Business.Service.Base;
using Business.Service.Customer;
using Demo.Api.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.EF;
using Repository.Entity.AuthModel;
using Repository.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 读取配置
            JWTConfig config = new JWTConfig();
            Configuration.GetSection("JWT").Bind(config);
            #endregion

            services.Configure<JWTConfig>(Configuration.GetSection("JWT"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config.Issuer,
                    ValidAudience = config.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.IssuerSigningKey)),
                    ClockSkew = TimeSpan.FromMinutes(1),
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    SaveSigninToken = false,
                    ValidateActor = false,
                    ValidateAudience = true,  //是否验证接受者
                    ValidateIssuer = true,   //是否验证发布者
                    ValidateIssuerSigningKey = true,  //是否验证秘钥
                    ValidateLifetime = true, //是否验证过期时间
                    ValidateTokenReplay = false,

                    /**关于JWT认证，这里通过options.TokenValidationParameters对认证信息做了设置
                     * ValidIssuer、ValidAudience、IssuerSigningKey这三个参数用于验证Token生成的时候填写的Issuer、Audience、IssuerSigningKey，
                     * 所以值要和生成Token时的设置一致
                     */


                    /**
                     * ClockSkew默认值为5分钟，它是一个缓冲期，例如Token设置有效期为30分钟，到了30分钟的时候是不会过期的，会有这么个缓冲时间，也就是35分钟才会过期。
                     * 为了方便测试（不想等太长时间），这里我设置了1分钟。
                     */
                };
                //如果jwt过期，在返回的header中加入Token-Expired字段为true，前端在获取返回header时判断
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddCors(options =>
            options.AddPolicy("AllowAnyOrigin", p =>
             //指定确切的地址与端口
             //p.WithOrigins(new string[] { "http://127.0.0.1:5500" })
             //  .AllowAnyMethod()
             //  .AllowAnyHeader()
             //  .AllowCredentials()
             // )
             //对跨域不做任何限制
             p.AllowAnyMethod()
                .SetIsOriginAllowed(p => true)
                .AllowAnyHeader()
                .AllowCredentials()

            ));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
            
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo.Api", Version = "v1", Description=".NET 6 WebApi" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Demo.Api.xml");
                c.IncludeXmlComments(xmlPath);
                //启用swagger验证功能
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //添加全局安全条件
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"}
                    },new string[] { }
                }
            });
                //显示自定义的Heard Token
                //c.OperationFilter<AuthTokenHeaderParameter>();
            });
            services.AddScoped<DbContext, MySqlDBContext>();
            services.AddDbContext<MySqlDBContext>(options =>
            {
                options.UseMySql(
                           Configuration.GetConnectionString("mysql"),
                           ServerVersion.AutoDetect(Configuration.GetConnectionString("mysql")),
                           option =>
                           {
                           });
            });


            services.AddScoped<IRepositoryFactory, RepositoryFactory>();//ע�����ݹ���
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IComsumeService, ComsumeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Demo.Api v1");
                });
        }
    }
}
