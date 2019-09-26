using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoService _photoService;

        public HomeController(IPhotoService photoService)
        {
            this._photoService = photoService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Photos = _photoService.RetrieveAllPhotos();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(HomeIndexViewModel vm)
        {
            if(string.IsNullOrWhiteSpace(vm.SearchKey))
                vm.Photos = _photoService.RetrieveAllPhotos();

            vm.Photos = _photoService.SearchPhotoByTitle(vm.SearchKey);

            return View(vm);
        }
    }
}