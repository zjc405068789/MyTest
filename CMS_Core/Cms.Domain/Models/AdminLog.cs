using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Domain.Models
{
    /// <summary>
    /// 管理后台日志
    /// </summary>
    public class AdminLog
    {
        public int AdminLog_Id { get; set; }

        public int OperateUsers_Id { get; set; }

        public string LoginIP { get; set; }

        public string Operation { get; set; }

        public string Remark { get; set; }

        public DateTime AddTime { get; set; }
    }
}
