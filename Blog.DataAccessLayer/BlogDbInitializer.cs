using Blog.Entities;
using System;
using System.Data.Entity;

namespace Blog.DataAccessLayer
{
    public class BlogDbInitializer : CreateDatabaseIfNotExists<BlogDbContext>
    {
        protected override void Seed(BlogDbContext context)
        {
            Random rnd = new Random();
            int categoryCount = 6;
            int userCount = 11;
            int postCount = 15;

            #region Add Users

            User admin = new User()
            {
                Username = "admin",
                Email = "admin@anhblog.com",
                Password = "password",
                IsActive = true,
                IsAdmin = true,
                ActivationGuid = Guid.NewGuid(),
                CreationDate = DateTime.Now,
            };

            context.Users.Add(admin);

            for (int i = 1; i < userCount; i++)
            {
                User u = new User()
                {
                    Username = FakeData.NameData.GetFullName().Replace(" ", "").ToLower(),
                    Email = FakeData.NetworkData.GetEmail(),
                    Password = FakeData.TextData.GetAlphaNumeric(10),
                    IsActive = false,
                    IsAdmin = false,
                    ActivationGuid = Guid.NewGuid(),
                    CreationDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddDays(-3),DateTime.Now)
                };

                context.Users.Add(u);
                System.Threading.Thread.Sleep(10); // to make sure FakeData gets different values.
            }

            context.SaveChanges();

            #endregion

            #region Add Categories

            context.Categories.AddRange(new Category[]
            {
                new Category { Name = "Technology", Description = FakeData.TextData.GetSentences(1), CreationDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddDays(-10),DateTime.Now.AddDays(-5))},
                new Category { Name = "Science", Description = FakeData.TextData.GetSentences(1), CreationDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddDays(-10),DateTime.Now.AddDays(-5))},
                new Category { Name = "Linguistics", Description = FakeData.TextData.GetSentences(1), CreationDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddDays(-10),DateTime.Now.AddDays(-5))},
                new Category { Name = "Health", Description = FakeData.TextData.GetSentences(1), CreationDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddDays(-10),DateTime.Now.AddDays(-5))},
                new Category { Name = "Games", Description = FakeData.TextData.GetSentences(1), CreationDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddDays(-10),DateTime.Now.AddDays(-5))},
                new Category { Name = "Cooking", Description = FakeData.TextData.GetSentences(1), CreationDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddDays(-10),DateTime.Now.AddDays(-5))}
            });

            context.SaveChanges();

            #endregion

            #region Add Posts

            for (int i = 1; i < postCount+1; i++)
            {
                bool draft = i % 3 == 0 ? false : true;

                string title = FakeData.TextData.GetSentence();
                if (title.Length >= 60)
                    title = title.Substring(0, 59);

                Post p = new Post()
                {
                    Title = title,
                    Text = FakeData.TextData.GetSentences(15),
                    IsDraft = draft,
                    Category = context.Categories.Find(rnd.Next(1,categoryCount)),
                    User = context.Users.Find(rnd.Next(1,userCount))
                };
                p.CreationDate = p.User.CreationDate.AddHours(rnd.Next(0, 10));

                context.Posts.Add(p);
                System.Threading.Thread.Sleep(10); // to make sure FakeData gets different values.
            }

            context.SaveChanges();

            #endregion

            #region Add Comments

            for (int i = 0; i < 50; i++)
            {
                Comment comment = new Comment()
                {
                    Rating = rnd.Next(1, 5),
                    Text = FakeData.TextData.GetSentences(3),
                    User = context.Users.Find(rnd.Next(1, userCount)),
                    Post = context.Posts.Find(rnd.Next(1, postCount))
                };
                comment.CreationDate = comment.Post.CreationDate.AddHours(rnd.Next(0, 10));

                context.Comments.Add(comment);
                System.Threading.Thread.Sleep(10); // to make sure FakeData gets different values.
            }

            context.SaveChanges();

            #endregion
        }
    }
}
