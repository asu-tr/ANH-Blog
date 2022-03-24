using Blog.Entities;
using System.Data.Entity;

namespace Blog.DataAccessLayer
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {
            Database.SetInitializer(new BlogDbInitializer());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
