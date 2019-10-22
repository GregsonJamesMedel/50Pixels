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

        public ApplicationUser ApplicationUser { get; set; }
        public IFormFile Photo { get; set; }
    }
}