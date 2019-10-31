using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPhotoService _photoService;

        public SearchController(IPhotoService photoService)
        {
            this._photoService = photoService;
        }

        public IActionResult Results(string SearchKey)
        {
            var vm = new SearchResultsViewModel();
            vm.SearchKey = SearchKey;
            vm.Photos = this._photoService.SearchPhotoByTitle(SearchKey);
            return View(vm);
        }
    }
}