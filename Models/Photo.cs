using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _50Pixels.Models
{
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Path { get; set; }

        public int Views { get; set; }
        public DateTime DateUploaded { get; set; }
        public string UploaderId { get; set; }
        
        [ForeignKey("UploaderId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}