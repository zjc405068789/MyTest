using Cms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.IRepository
{
    public interface IAdministratorRepository
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="_username">用户名</param>
        /// <param name="_password">密码</param>
        /// <returns></returns>
        Administrator Login(string _username, string _password);
    }
}
