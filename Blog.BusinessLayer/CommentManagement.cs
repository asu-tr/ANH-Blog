using Blog.DataAccessLayer;
using Blog.Entities;
using System.Linq;

namespace Blog.BusinessLayer
{
    public class CommentManagement
    {
        Repository<Comment> repoComment = new Repository<Comment>();
        ResultManagement<Comment> rm = new ResultManagement<Comment>();

        public ResultManagement<Comment> Delete(Comment comment)
        {
            Comment c = repoComment.Find(x => x.Id == comment.Id);
            if (c != null)
            {
                int result = repoComment.Delete(c);

                if (result == 0)
                {
                    rm.Results.Add("Comment couldn't be deleted.");
                    return rm;
                }
            }
            else
                rm.Results.Add("Comment not found.");

            return rm;
        }

        public IQueryable<Comment> ListQueryable()
        {
            return repoComment.ListQueryable();
        }
    }
}