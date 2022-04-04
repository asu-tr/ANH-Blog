using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities
{
    public class User : EntitiesBase
    {
        [Required, StringLength(25, ErrorMessage = "{0} needs to be max {1} characters.")]
        public string Username { get; set; }

        [StringLength(40)]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(24)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public Guid ActivationGuid { get; set; }

        [StringLength(30)]
        public string ProfileImageFile { get; set; }

        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}