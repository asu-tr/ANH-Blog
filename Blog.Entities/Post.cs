using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities
{
    public class Post : EntitiesBase
    {
        [Required, StringLength(50)]
        public string Title { get; set; }
        [Required, StringLength(2000)]
        public string Text { get; set; }
        public bool IsDraft { get; set; }

        public virtual Category Category { get; set; }  
        public virtual User User { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
