using System.Collections.Generic;
using _50Pixels.Models;

namespace _50Pixels.ViewModels
{
    public class SearchResultsViewModel
    {
        public string SearchKey { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}