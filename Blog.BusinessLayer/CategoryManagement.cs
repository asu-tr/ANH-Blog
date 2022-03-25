using Blog.DataAccessLayer;
using Blog.Entities;
using System.Collections.Generic;

namespace Blog.BusinessLayer
{
    public static class CategoryManagement
    {
        private static Repository<Category> repoCat = new Repository<Category>();

        public static List<Category> GetList()
        {
            return repoCat.List();
        }

        public static Category GetCategory(int id)
        {
            return repoCat.Find(x => x.Id == id);
        }
    }
}
