using Cms.App.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.App.Models
{
    public class JsonResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public StatusCode Code { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }
    }
}
