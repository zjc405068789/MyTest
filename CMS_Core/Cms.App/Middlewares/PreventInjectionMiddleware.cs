using Cms.Common.Result;
using Cms.Common.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.App.Middlewares
{
    /// <summary>
    /// 过滤危险字符
    /// </summary>
    public class PreventInjectionMiddleware
    {
        const string killSqlFilter = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|cmd";
        private readonly RequestDelegate _next;

        public PreventInjectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //客户端发起请求的时候，系统默认调用该方法
        public async Task Invoke(HttpContext context)
        {
            var flag = false;
            if (context.Request.HasFormContentType && context.Request.Form != null)
            {
                foreach (var i in context.Request.Form)
                {
                    if (TextHelper.SqlFilter(killSqlFilter, i.Value))
                    {
                        flag = true;
                        break;
                    }
                }
            }

            if (!flag)
            {
                foreach (var i in context.Request.Query)
                {
                    if (TextHelper.SqlFilter(killSqlFilter, i.Value))
                    {
                        flag = true;
                        break;
                    }
                }
            }

            if (flag)
            {
                await context.Response.WriteCustomContentAsync(new SysResult<object>
                {
                    StatusCode = SysStatusCode.illegal_character
                });

            }
            else
            {
                // Call the next delegate/middleware in the pipeline
                await this._next(context);
            }
        }
    }
    public static class PreventInjectionMiddlewareeExtensions
    {
        /// <summary>
        /// 启用过滤危险字符
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePreventInjection(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PreventInjectionMiddleware>();
        }
    }
}
