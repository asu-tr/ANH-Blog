using Blog.BusinessLayer;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ANH_Blog.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category cat)
        {
            ModelState.Remove("CreationDate");

            if (ModelState.IsValid)
            {
                ResultManagement<Category> rm = CategoryManagement.Save(cat);

                if (rm.Results.Count > 0)
                {
                    rm.Results.ForEach(x => ModelState.AddModelError("", x));
                    return View(cat);
                }

                return RedirectToAction("Index");
            }

            return View(cat);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category cat = CategoryManagement.FindById(id.Value);

            if (cat == null)
                return HttpNotFound();

            return View(cat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category cat = CategoryManagement.FindById(id);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Category cat = CategoryManagement.FindById(id.Value);

            if (cat == null)
                return HttpNotFound();

            return View(cat);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Category cat = CategoryManagement.FindById(id.Value);

            if (cat == null)
                return HttpNotFound();

            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category cat)
        {
            ModelState.Remove("CreationDate");

            if (ModelState.IsValid)
            {
                ResultManagement<Category> rm = CategoryManagement.Update(cat);

                if (rm.Results.Count > 0)
                {
                    rm.Results.ForEach(x => ModelState.AddModelError("", x));
                    return View(cat);
                }

                return RedirectToAction("Index");
            }

            return View(cat);
        }

        public ActionResult Index()
        {
            return View(CategoryManagement.GetList());
        }
    }
}