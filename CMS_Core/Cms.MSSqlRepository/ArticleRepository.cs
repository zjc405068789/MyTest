using Cms.Domain.Models;
using Cms.IRepository;
using Dapper;
using System.Collections.Generic;

namespace Cms.MSSqlRepository
{
    public class ArticleRepository : BaseRepository, IArticleRepository
    {
        public IList<Article> GetList()
        {
            using (var dbcon = Create_gudashi_Connection())
            {
                var list = dbcon.Query<Article>("SELECT top 10 * FROM dbo.Article").AsList();
                return list;
            }
        }
    }
}