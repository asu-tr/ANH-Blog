using Blog.BusinessLayer;
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
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Category(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Category category = CategoryManagement.GetCategory(id.Value);
            return View("Index", category.Posts);
        }

        public ActionResult Index()
        {
            //old codes
            //Blog.BusinessLayer.Test test = new Blog.BusinessLayer.Test();
            //test.InsertTest();
            //test.UpdateTest();

            PostManagement pm = new PostManagement();

            return View(pm.GetList().OrderByDescending(p => p.CreationDate).Take(9).ToList());
        }

        public ActionResult Popular()
        {
            PostManagement pm = new PostManagement();

            return View ("Index", pm.GetList().OrderByDescending(p => p.Comments.Count).Take(6).ToList());
        }
    }
}