using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace _50Pixels.ViewModels
{
    public class AccountRegisterViewModel
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

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [MinLength(5)]
        [MaxLength(20)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Firstname { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Lastname { get; set; }
        public IFormFile Photo { get; set; }
    }
}