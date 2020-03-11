using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Domain.Const;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpResponseExtensions
    {

        /// <summary>
        /// 写入自定义对象
        /// </summary>
        /// <param name="response"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task WriteCustomContentAsync(this HttpResponse response, object content, string contentType = HttpContentTypeConst.APPLICATION_JSON)
        {
            response.Clear();
            response.ContentType = contentType;
            await response.WriteAsync(JsonConvert.SerializeObject(content));
        }

    }
}