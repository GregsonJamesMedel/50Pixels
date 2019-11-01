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

        public IActionResult Results(SearchResultsViewModel searchVM)
        {
            searchVM.Photos = this._photoService.SearchPhotoByTitle(searchVM.SearchKey);
            return View(searchVM);
        }
    }
}