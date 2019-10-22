using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _50Pixels.Models;
using Microsoft.AspNetCore.Http;

namespace _50Pixels.ViewModels
{
    public class ManageAccountVM
    {
        // [Required]
        // [MinLength(2)]
        // [MaxLength(20)]
        public string Firstname { get; set; }

        // [Required]
        // [MinLength(2)]
        // [MaxLength(20)]
        public string Lastname { get; set; }

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
        public ApplicationUser ApplicationUser { get; set; }
        public IFormFile Photo { get; set; }
    }
}