using System.Collections.Generic;

namespace Blog.Entities
{
    public class Category : EntitiesBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
