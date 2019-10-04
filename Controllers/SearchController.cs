using System.Linq;
using _50Pixels.Services;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace _50Pixels.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPhotoService _photoService;

        public SearchController(IPhotoService photoService)
        {
            this._photoService = photoService;
        }

        public IActionResult Results(string SearchKey, int page = 1)
        {
            var photos = _photoService.SearchPhotoByTitle(SearchKey);
            var model = PagingList.Create(photos,photos.Count(),page);
            return View(model);
        }
    }
}