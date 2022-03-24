using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ANH_Blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Blog.BusinessLayer.Test test = new Blog.BusinessLayer.Test();

            return View();
        }
    }
}