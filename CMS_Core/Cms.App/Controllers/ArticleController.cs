using Cms.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cms.App.Controllers
{
    public class ArticleController : Controller
    {
        IArticleRepository _articleRepository;
        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IActionResult Index()
        {
            return Content("Page Article");
        }

        public string GetPageList()
        {
            var b = 0;
            var c = 5 / b;
            return "";
        }

        public string GetList()
        {
            var list = _articleRepository.GetList();
            return JsonConvert.SerializeObject(list);
        }

        public string GetTestData()
        {
            return "mytest";
        }
    }
}