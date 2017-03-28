using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _491Project.Areas.App.Controllers
{
    public class IndexController : Controller
    {   
        /// <summary>
        /// GET: App/Index
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }
    }
}