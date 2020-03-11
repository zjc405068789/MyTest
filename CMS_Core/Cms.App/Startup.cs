using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.App.Config;
using Cms.App.Filters;
using Cms.App.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Cms.App
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDistributedMemoryCache();//启用session之前必须先添加内存
            services.AddSession(option =>
            {
                option.Cookie.Name = ".AdventureWorks.Session";
                option.IdleTimeout = TimeSpan.FromHours(2); //设置session的过期时间
                option.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            });

            services.AddMemoryCache();//启用内存缓存

            services.AddService(); //配置依赖注入

            services.AddCors(options => //配置跨域
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();//全局异常过滤器

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseCors("AllowAll"); //启用跨域
            app.UsePreventInjection(); //启用危险字符过滤
            loggerFactory.AddNLog(); //启用nlog
            app.UseSession();
            app.UseMyExceptionHandler();//启用请求截获

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
