using Cms.Domain.Models;
using Cms.IRepository;
using Cms.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cms.Service
{
    public class ArticleService : IArticleService
    {
        IArticleRepository _articleRepository;
        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public List<Article> GetList()
        {
            return _articleRepository.GetList().ToList();
        }
    }
}
