using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.App.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cms.App.Models;
using Cms.App.Extensions;

namespace Cms.App.Areas.Admin.Controllers
{
    [LogAttribute]
    [Area("Admin")]
    public class AdminBaseController : Controller
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult JsonSuccess()
        {
            var r = new JsonResult<object>
            {
                Code = Enumerate.StatusCode.Success
            };

            return Content(JsonConvert.SerializeObject(r), "application/json");
        }

        /// <summary>
        /// 错误结果
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected IActionResult JsonError(string message = null)
        {
            var r = new JsonResult<object>
            {
                Code = Enumerate.StatusCode.Failure
            };

            if (message == null)
            {
                r.Message = r.Code.GetDescription();
            }
            else
            {
                r.Message = message;
            }
            return Content(JsonConvert.SerializeObject(r), "application/json");
        }
    }
}