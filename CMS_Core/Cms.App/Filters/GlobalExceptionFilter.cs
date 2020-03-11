using Cms.Common.Result;
using Cms.Common.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cms.App.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IHostingEnvironment _environment;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        //全局异常处理
        public void OnException(ExceptionContext context)
        {
            //记录日志
            _logger.LogError(context.Exception, JsonConvert.SerializeObject(
                new
                {
                    DataSource = context.Exception.Source,
                    HttpRequestInfo = HttpHelper.GetRequestParams(context.HttpContext)
                }));
            if (!_environment.IsDevelopment()) //判断是否为开发环境
            {
                context.Result = new JsonResult(new Common.Result.SysResult<object>
                {
                    StatusCode = SysStatusCode.sys_fail
                });
                context.ExceptionHandled = true; //异常已处理
            }

        }
    }
}
