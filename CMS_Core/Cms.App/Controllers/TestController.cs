using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Common.Util;
using Microsoft.AspNetCore.Mvc;

namespace Cms.App.Controllers
{
    public class TestController : Controller
    {
        public IActionResult DESEncrypt(string value)
        {
            return Content(CryptoHelper.DESEncrypt(value));
        }

        public IActionResult DESDecrypt(string Value)
        {
            return Content(CryptoHelper.DESDecrypt(Value));
        }
    }
}