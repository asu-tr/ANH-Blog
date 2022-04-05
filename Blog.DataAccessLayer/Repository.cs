using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.DataAccessLayer
{
    // Design Pattern: Repository Pattern
    // Insert, Update, Delete, List
    public class Repository<T> where T : class
    {
        BlogDbContext db;
        private DbSet<T> _dbSet;

        public Repository()
        {
            db = Singleton.CreateContext();
            _dbSet = db.Set<T>();
        }

        public List<T> List()
        {
            return _dbSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> exp)
        {
            return _dbSet.Where(exp).ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        public int Insert(T obj)
        {
            _dbSet.Add(obj);

            if (obj is EntitiesBase)
            {
                EntitiesBase obj2 = obj as EntitiesBase;
                obj2.CreationDate = DateTime.Now;
            }
            return Save();
        }

        private int Save()
        {
            return db.SaveChanges();
        }

        public int Delete(T obj)
        {
            _dbSet.Remove(obj);
            return Save();
        }

        public int Update()
        {
            return Save();
        }

        public T Find(Expression<Func<T,bool>> exp)
        {
            T obj = _dbSet.FirstOrDefault(exp);
            return obj;
        }
    }
}
