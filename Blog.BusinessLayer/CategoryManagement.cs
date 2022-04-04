using Blog.DataAccessLayer;
using Blog.Entities;
using System.Collections.Generic;

namespace Blog.BusinessLayer
{
    public static class CategoryManagement
    {
        private static Repository<Category> repoCat = new Repository<Category>();
        private static ResultManagement<Category> rm = new ResultManagement<Category>();

        public static List<Category> GetList()
        {
            return repoCat.List();
        }

        public static Category FindById(int id)
        {
            return repoCat.Find(x => x.Id == id);
        }

        public static ResultManagement<Category> Save(Category model)
        {
            rm.Obj = repoCat.Find(x => x.Name == model.Name);

            if (rm.Obj != null)
                rm.Results.Add("This category exists.");

            else
            {
                int result = repoCat.Insert(new Category()
                {
                    Name = model.Name,
                    Description = model.Description
                });
            }
            return rm;
        }

        public static ResultManagement<Category> Update(Category model)
        {
            Category cat = repoCat.Find(x => x.Name == model.Name && x.Id != model.Id);

            if (cat != null)
                rm.Results.Add("Bu kategori kayıtlı");

            else
            {
                rm.Obj = repoCat.Find(x => x.Id == model.Id);
                rm.Obj.Name = model.Name;
                rm.Obj.Description = model.Description;

                int result = repoCat.Update();

                if (result > 0)
                    rm.Obj = repoCat.Find(x => x.Id == model.Id);
            }
            return rm;
        }
    }
}
