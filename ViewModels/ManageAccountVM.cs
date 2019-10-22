using _50Pixels.Models;
using Microsoft.AspNetCore.Http;

namespace _50Pixels.ViewModels
{
    public class ManageAccountVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IFormFile Photo { get; set; }
    }
}