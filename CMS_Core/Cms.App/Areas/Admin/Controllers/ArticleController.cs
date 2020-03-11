using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cms.App.Areas.Admin.Controllers
{
    public class ArticleController : AdminBaseController
    {
        public IActionResult Index()
        {
            return Content("Admin Article");
        }
    }
}