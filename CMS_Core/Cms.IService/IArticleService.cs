using Cms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.IService
{
    public interface IArticleService
    {
        List<Article> GetList();
    }
}
