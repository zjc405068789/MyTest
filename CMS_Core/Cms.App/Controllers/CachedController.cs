using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.App.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Cms.App.Controllers
{
    [TypeFilter(typeof(NaiveCacheResourceFilterAttribute))]
    public class CachedController : Controller
    {
        public IActionResult Index()
        {
            return Content("This content was generated at " + DateTime.Now);
        }
    }
}