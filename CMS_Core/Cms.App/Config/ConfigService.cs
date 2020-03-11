using Cms.IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.MSSqlRepository;
using Cms.IService;
using Cms.Service;

namespace Cms.App.Config
{
    public static class ConfigService
    {
        public static void AddService(this IServiceCollection services)
        {
            #region repository
            services.AddScoped<IArticleRepository, ArticleRepository>();
            #endregion

            #region service
            services.AddScoped<IArticleService, ArticleService>();
            #endregion
        }
    }
}
