using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities
{
    public class EntitiesBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
