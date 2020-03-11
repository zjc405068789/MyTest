using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Cms.Common.Util;
using MySql.Data.MySqlClient;

namespace Cms.MySqlRepository
{
    public abstract class BaseRepository
    {
        public static IDbConnection CreateCmsConnection()
        {
            return new MySqlConnection(ConfigurationHelper.GetConnectionString("mySqlConStr_cms2"));
        }

        public static IDbConnection CreateCmslogConnection()
        {
            return new MySqlConnection(ConfigurationHelper.GetConnectionString("mySqlConStr_cmslog"));
        }
    }
}
