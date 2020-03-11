using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.App.Enumerate
{
    public enum StatusCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        Success = 200,

        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("操作失败")]
        Failure = 2,

        /// <summary>
        /// 服务器错误
        /// </summary>
        [Description("服务器错误")]
        ServerError = 500,

        /// <summary>
        /// 其他错误
        /// </summary>
        [Description("其他错误")]
        Other = 600,

        /// <summary>
        /// 自定义错误
        /// </summary>
        [Description("自定义错误")]
        Custom = 700
    }
}
