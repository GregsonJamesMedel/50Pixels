using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace _50Pixels.ViewModels
{
    public class UploadPhotoViewModel
    {
        [Required]
        public string Title { get; set; }

        //[RegularExpression("(.*png$)|(.*jpg$)|(.*jpeg$)",ErrorMessage="only image files are allowed to upload")]
        public IFormFile Photo { get; set; }
    }
}