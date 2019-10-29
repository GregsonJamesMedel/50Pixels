using System.ComponentModel.DataAnnotations;

namespace _50Pixels.Models
{
    public class Follow
    {
        public int Id { get; set; }

        [Required]
        public string Following { get; set; }

        [Required]
        public string Follower { get; set; }
    }
}