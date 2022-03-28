using Blog.BusinessLayer;
using Blog.DataAccessLayer;
using Blog.Entities;
using System.Linq;
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

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User u)
        {
            string username = u.Username;
            string password = u.Password;

            BlogDbContext context = new BlogDbContext();

            int count = context.Users.Where(s => s.Username == username && s.Password == password).ToList().Count;
            if (count == 1)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public ActionResult Popular()
        {
            PostManagement pm = new PostManagement();

            return View ("Index", pm.GetList().OrderByDescending(p => p.Comments.Count).Take(6).ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User u)
        {
            Repository<User> repoUser = new Repository<User>();
            User user = repoUser.Find(x => x.Username == u.Username || x.Email == u.Email);

            if (ModelState.IsValid)
            {
                UserManagement um = new UserManagement();
                ErrorLayer err = um.SaveUser(u);
            }
            else
            {

            }

            return View();
        }
    }
}