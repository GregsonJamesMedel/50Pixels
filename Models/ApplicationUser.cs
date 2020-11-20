using System;
using Microsoft.AspNetCore.Identity;

namespace _50Pixels.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhotoPath { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}