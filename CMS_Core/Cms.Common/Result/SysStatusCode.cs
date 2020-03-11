using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cms.Common.Result
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum SysStatusCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功.")]
        sys_success = 0,

        /// <summary>
        /// 服务器异常
        /// </summary>
        [Description("服务器异常.")]
        sys_fail = 1,

        /// <summary>
        /// 参数值格式有误
        /// </summary>
        [Description("参数值格式有误.")]
        sys_param_format_error = 2,

        /// <summary>
        /// 相关资源不存在
        /// </summary>
        [Description("相关资源不存在.")]
        sys_correlation_resource_no_exist = 3,

        /// <summary>
        /// token无效
        /// </summary>
        [Description("token无效.")]
        token_error = 4,

        /// <summary>
        /// 非法字符
        /// </summary>
        [Description("包含非法字符.")]
        illegal_character = 12,

        /// <summary>
        /// 验证失败
        /// </summary>
        [Description("验证失败.")]
        verify_fail = 13,

        /// <summary>
        /// 预留错误码4
        /// </summary>
        user_Reserved_4 = 14,

        /// <summary>
        /// 预留错误码5
        /// </summary>
        user_Reserved_5 = 15,

        /// <summary>
        /// 预留错误码6
        /// </summary>
        user_Reserved_6 = 16,

        /// <summary>
        /// 预留错误码7
        /// </summary>
        user_Reserved_7 = 17,

        /// <summary>
        /// 预留错误码8
        /// </summary>
        user_Reserved_8 = 18,

        /// <summary>
        /// 用户自定义错误码
        /// </summary>
        user_custom = 99,
    }
}
