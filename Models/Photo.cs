using System.ComponentModel.DataAnnotations;

namespace _50Pixels.Models
{
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Path { get; set; }
    }
}