using Blog.BusinessLayer;
using Blog.Entities;
using Blog.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ANH_Blog.Controllers
{
    public class HomeController : Controller
    {
        UserManagement um = new UserManagement();

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ActivateUser(Guid id)
        {
            ResultManagement<User> result = um.ActivateUser(id);

            if (result.Results.Count > 0)
            {
                TempData["Error"] = result.Results;
                return RedirectToAction("ActivationError");
            }

            return RedirectToAction("ActivationOK");
        }

        public ActionResult ActivationError()
        {
            List<string> errors = new List<string>();
            if (TempData["Error"] != null)
            {
                errors = (List<string>)TempData["Error"];
            }
            return View(errors);
        }

        public ActionResult ActivationOK()
        {
            return View();
        }

        public ActionResult Category(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Category category = CategoryManagement.GetCategory(id.Value);
            return View("Index", category.Posts);
        }

        public ActionResult Index()
        {
            PostManagement pm = new PostManagement();
            return View(pm.GetList().OrderByDescending(p => p.CreationDate).Take(9).ToList());
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInVM model)
        {
            if (ModelState.IsValid)
            {
                UserManagement um = new UserManagement();
                ResultManagement<User> result = um.LogIn(model);

                if (result.Results.Count > 0)
                {
                    result.Results.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                Session["login"] = result.Obj;
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Popular()
        {
            PostManagement pm = new PostManagement();
            return View ("Index", pm.GetList().OrderByDescending(p => p.Comments.Count).Take(6).ToList());
        }

        public ActionResult ProfileDelete()
        {
            User user = (User)Session["login"];

            ResultManagement<User> result = um.DeleteUser(user.Id);

            if (result.Results.Count > 0)
            {
                // Redirect to error page
            }
            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult ProfileEdit()
        {
            User user = (User)Session["login"];
            ResultManagement<User> result = um.GetUser(user.Id);

            if (result.Results.Count > 0)
            {
                // Redirect if there are some errors.
            }

            return View(result.Obj);
        }

        [HttpPost]
        public ActionResult ProfileEdit(User user, HttpPostedFileBase profileimage)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (profileimage != null && (profileimage.ContentType == "image/png" || profileimage.ContentType == "image/jpg" || profileimage.ContentType == "image/jpeg"))
                {
                    string fileName = $"user_{user.Id}.{profileimage.ContentType.Split('/')[1]}"; //user_1.jpg
                    profileimage.SaveAs(Server.MapPath($"~/Image/{fileName}"));
                    user.ProfileImageFile = fileName;
                }

                ResultManagement<User> result = um.UpdateUser(user);

                if (result.Results.Count > 0)
                {
                    // return
                }

                Session["login"] = result.Obj;

                return RedirectToAction("ProfileShow");
            }
            return View(user);
        }

        public ActionResult ProfileShow()
        {
            User user = (User)Session["login"];
            ResultManagement<User> result = um.GetUser(user.Id);

            if (result.Results.Count > 0)
            {
                // Redirect if there are some errors.
            }

            return View(result.Obj);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                UserManagement um = new UserManagement();
                ResultManagement<User> result = um.SaveUser(model);

                if (result.Results.Count > 0)
                {
                    result.Results.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                return RedirectToAction("RegisterOK");
            }
            return View();
        }

        public ActionResult RegisterOK()
        {
            return View();
        }
    }
}