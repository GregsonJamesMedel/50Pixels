using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _50Pixels.ViewModels
{
    public class AccountSignInViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        [MaxLength(20)] 
        public string Password { get; set; }
    }
}