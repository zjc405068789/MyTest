using Cms.Domain.Models;
using Cms.IRepository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.MySqlRepository
{
    public class ArticleRepository : BaseRepository, IArticleRepository
    {
        public IList<Article> GetList()
        {
            using (var dbcon = CreateCmsConnection())
            {
                var list = dbcon.Query<Article>("SELECT * FROM cms.article").AsList();
                return list;
            }
        }
    }
}
