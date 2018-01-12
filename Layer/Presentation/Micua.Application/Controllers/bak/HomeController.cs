using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Micua.Domain.Model;
using Micua.Domain.Service;

namespace Micua.Application.Controllers.bak
{
    public class HomeController : Controller
    {
        [Microsoft.Practices.Unity.Dependency]
        public IUserService UserService { get; set; }

        public ActionResult Index()
        {
            return View();
        }
    }
}