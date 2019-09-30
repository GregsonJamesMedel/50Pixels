using System.Collections.Generic;
using _50Pixels.Models;

namespace _50Pixels.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Photo> Photos { get; set; }
        public string SearchKey { get; set; }
    }
}