using Blog.BusinessLayer;
using Blog.Entities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ANH_Blog.Controllers
{
    public class PostController : Controller
    {
        PostManagement pm = new PostManagement();
        CommentManagement cm = new CommentManagement();

        public ActionResult Comments()
        {
            User user = (User)Session["login"];

            var posts = cm.ListQueryable().Include("User").Include("Post").Where(x => x.User.Id == user.Id)
                .Select(x => x.Post).Include("Category").Include("User").OrderByDescending(x => x.CreationDate);

            return View("Index", posts.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Index");

            return View(post);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Post post = pm.FindById(id.Value);
            if (post == null)
                return HttpNotFound();

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = pm.FindById(id);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Post post = pm.FindById(id.Value);

            if (post == null)
                return HttpNotFound();

            return View(post);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Post post = pm.FindById(id.Value);
            if (post == null)
                return HttpNotFound();

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Index");

            return View(post);
        }

        public ActionResult Index()
        {
            User user = (User)Session["login"];

            return View(pm.ListQueryable().Include("Category").Include("User").Where(x => x.User.Id == user.Id).OrderByDescending(x => x.CreationDate).ToList());
        }
    }
}