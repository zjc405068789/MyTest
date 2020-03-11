using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cms.App.Extensions;
using Microsoft.AspNetCore.Http;
using Cms.IRepository;
using Cms.Common.Util;
using Cms.Domain.Models;

namespace Cms.App.Areas.Admin.Controllers
{
    public class LoginController : AdminBaseController
    {
        IAdministratorRepository _administratorRepository;
        public LoginController(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string username, string password)
        {
            password = CryptoHelper.DESEncrypt(password);
            var model = _administratorRepository.Login(username, password);
            if (model != null)
            {
                HttpContext.Session.Set<Administrator>("user", model);
                return JsonSuccess();
            }
            else
            {
                return JsonError("账号或者密码错误");
            }
        }
    }
}