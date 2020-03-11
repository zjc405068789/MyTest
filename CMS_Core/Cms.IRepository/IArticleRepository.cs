using Cms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.IRepository
{
    public interface IArticleRepository
    {
        IList<Article> GetList();
    }
}
