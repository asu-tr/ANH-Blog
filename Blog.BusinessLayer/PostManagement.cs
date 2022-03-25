using Blog.DataAccessLayer;
using Blog.Entities;
using System.Collections.Generic;

namespace Blog.BusinessLayer
{
    public class PostManagement
    {
        Repository<Post> repoPost = new Repository<Post>();

        public List<Post> GetList()
        {
            return repoPost.List();
        }
    }
}
