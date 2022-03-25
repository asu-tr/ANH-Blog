using Blog.DataAccessLayer;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BusinessLayer
{
    public class Test
    {
        Repository<User> repoUser = new Repository<User>();

        public Test()
        {
            // 1
            //db.Database.CreateIfNotExists();

            // 2
            // DataAccessLayer.BlogDbContext db = new DataAccessLayer.BlogDbContext();
            // db.Users.ToList();

            Repository<Category> repo = new Repository<Category>();
            List<Category> list = repo.List();
        }

        public int InsertTest()
        {
            int result = repoUser.Insert(new User()
            {
                Username = "test",
                Email = "test@anhblog.com",
                Password = "tteesstt",
                IsActive = true,
                IsAdmin = false,
                ActivationGuid = Guid.NewGuid(),
                CreationDate = DateTime.Now,
            });
            return result;
        }

        public int UpdateTest()
        {
            User testUser = repoUser.Find(x => x.Username == "test");
            testUser.Email = "testuser@anhblog.com";

            return repoUser.Update();
        }

        public int DeleteTest()
        {
            User testUser = repoUser.Find(x => x.Username == "test");
            if (testUser != null)
                return repoUser.Delete(testUser);
            else
                return 0;
        }
    }
}
