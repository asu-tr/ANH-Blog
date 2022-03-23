using System.Collections.Generic;

namespace Blog.Entities
{
    public class Post : EntitiesBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDraft { get; set; }

        public virtual Category Category { get; set; }  
        public virtual User User { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
