using System.ComponentModel.DataAnnotations.Schema;

namespace _50Pixels.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public string LikerId { get; set; }

        [ForeignKey("PhotoId")]
        public virtual Photo Photo { get; set; }

        [ForeignKey("LikerId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}