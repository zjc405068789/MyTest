using Cms.Domain.Models;
using Cms.MySqlRepository;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.App.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            #region 每一个请求都入库
            string path = "访问路径：" + context.HttpContext.Request.Host + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString.Value;
            var ip = context.HttpContext.Connection.RemoteIpAddress.ToString();

            AdminLog model = new AdminLog();
            model.AdminLog_Id = 0;
            model.LoginIP = ip;
            model.Operation = path;

            AdminLogRepository.CreateLog(model);
            #endregion
            base.OnActionExecuted(context);
        }
    }
}
