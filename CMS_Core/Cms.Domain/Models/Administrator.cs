using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Domain.Models
{
    /// <summary>
    /// 管理员表实体
    /// </summary>
   public class Administrator
    {
        public int Administrator_id { get; set; }

        /// <summary>
        /// 管理员-用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 管理员-密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string Addtime { get; set; }
    }
}
