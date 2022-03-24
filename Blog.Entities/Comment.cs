using System.ComponentModel.DataAnnotations;

namespace Blog.Entities

{
    public class Comment : EntitiesBase
    {
        [Required]
        public int Rating { get; set; }
        [Required, StringLength(240)]
        public string Text { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
