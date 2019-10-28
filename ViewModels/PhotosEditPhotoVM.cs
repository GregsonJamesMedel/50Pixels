using System.ComponentModel.DataAnnotations;

namespace _50Pixels.ViewModels
{
    public class PhotosEditPhotoVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string PhotoPath { get; set; }
    }
}