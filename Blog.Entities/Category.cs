using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities
{
    public class Category : EntitiesBase
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Description { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
