using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.ViewModels
{
    public class RegisterVM
    {
        [Required, StringLength(25)]
        public string Username { get; set; }

        [Required, StringLength(40),
            DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(24),
            DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Repeat Password"), Required,
            MinLength(8), MaxLength(24),
            DataType(DataType.Password),
            Compare("Password")]
        public string Password2 { get; set; }
    }
}