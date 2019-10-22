using System.ComponentModel.DataAnnotations;

namespace _50Pixels.ViewModels
{
    public class AccountManageChangeAccountDetailsVM
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Firstname { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Lastname { get; set; }
    }
}