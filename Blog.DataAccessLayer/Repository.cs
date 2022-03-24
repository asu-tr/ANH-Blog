using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccessLayer
{
    // Design Pattern: Repository Pattern
    // Insert, Update, Delete, List
    public class Repository<T> where T : class
    {
        BlogDbContext db = new BlogDbContext();
        public List<T> List()
        {
            return db.Set<T>().ToList();
        }

        public int Insert(T objj)
        {
            db.Set<T>().Add(objj);
            return Save();
        }

        private int Save()
        {
            return db.SaveChanges();
        }

        public int Delete(T objj)
        {
            db.Set<T>().Remove(objj);
            return Save();
        }

        public int Update()
        {
            return Save();
        }
    }
}
