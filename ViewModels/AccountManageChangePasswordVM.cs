using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _50Pixels.ViewModels
{
    public class AccountManageChangePasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        [MaxLength(20)]
        [DisplayName("Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        [MaxLength(20)]
        [DisplayName("New Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [MinLength(5)]
        [MaxLength(20)]
        [DisplayName("Confirm New Password")]
        public string ConfirmPassword { get; set; }
    }
}