using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.App.Models;
using Cms.App.Filters;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Cms.App.Controllers
{
    //[AddHeader("Zjc", "My Name")]
    public class HomeController : Controller
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public HomeController(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        //[ShortCircuitingResourceFilter]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MyError()
        {
            var b = 0;
            var c = 5 / b;
            return Content("");
        }

        public IActionResult MySleep()
        {
            Thread.Sleep(10 * 1000);
            return Content("MySleep");
        }

        public IActionResult MyInfo()
        {
            _logger.LogInformation("MyInfo");
            return Content("");
        }
    }
}
