using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Cms.Common.Util;

namespace Cms.MSSqlRepository
{
    public abstract class BaseRepository
    {
        public static IDbConnection Create_gudashi_Connection()
        {
            return new SqlConnection(ConfigurationHelper.GetConnectionString("gudashi"));
        }
    }
}
