using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccessLayer
{
    public static class Singleton
    {
        private static BlogDbContext db;
        private static object _lock = new object();

        public static BlogDbContext CreateContext()
        {
            lock (_lock)
            {
                if (db == null)
                {
                    db = new BlogDbContext();
                }
            }

            return db;
        }
    }
}
