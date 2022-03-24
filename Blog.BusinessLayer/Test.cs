using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BusinessLayer
{
    public class Test
    {
        DataAccessLayer.BlogDbContext db = new DataAccessLayer.BlogDbContext();

        public Test()
        {
           db.Database.CreateIfNotExists();
        }
    }
}
