using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cms.Common.Util
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Appsettings { get; private set; }
        static ConfigurationHelper()
        {
            Appsettings = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile("appsettings.Development.json", true, true)
                        .Build();

        }

        public static string GetConnectionString(string key)
        {
            return Appsettings.GetConnectionString(key);

        }
    }
}
