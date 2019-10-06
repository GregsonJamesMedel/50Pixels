using System.Collections.Generic;
using _50Pixels.Models;

namespace _50Pixels.ViewModels
{
    public class AccountProfileViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<Photo> LikedPhotos { get; set; }
        public bool IsCurrentUserProfile { get; set; }
    }
}