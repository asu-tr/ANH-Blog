using Blog.DataAccessLayer;
using Blog.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Blog.BusinessLayer
{
    public class PostManagement
    {
        Repository<Post> repoPost = new Repository<Post>();
        ResultManagement<Post> rm = new ResultManagement<Post>();

        public ResultManagement<Post> Delete(Post post)
        {
            Post p = repoPost.Find(x => x.Id == post.Id);
            if (p != null)
            {
                int result = repoPost.Delete(p);

                if (result == 0)
                {
                    rm.Results.Add("Post couldn't be deleted.");
                    return rm;
                }
            }
            else
                rm.Results.Add("Post not found.");
            return rm;
        }

        public Post FindById(int id)
        {
            return repoPost.Find(x => x.Id == id);
        }

        public List<Post> GetList()
        {
            return repoPost.List();
        }

        public IQueryable<Post> ListQueryable()
        {
            return repoPost.ListQueryable();
        }
    }
}