using Cms.Domain.Models;
using Cms.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace Cms.MySqlRepository
{
    public class AdminLogRepository : BaseRepository
    {
        public static void CreateLog(AdminLog model)
        {
            using (var dbcon = CreateCmslogConnection())
            {
                dbcon.ExecuteAsync("insert into adminlog(OperateUsers_Id,LoginIP,Operation,Remark) values(@OperateUsers_Id,@LoginIP,@Operation,@Remark)", new { OperateUsers_Id = model.OperateUsers_Id, LoginIP = model.LoginIP, Operation = model.Operation, Remark = model.Remark });
            }
        }
    }
}