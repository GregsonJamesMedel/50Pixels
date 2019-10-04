using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace _50Pixels.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoService _photoService;

        public HomeController(IPhotoService photoService)
        {
            this._photoService = photoService;
        }
        
        public IActionResult Index(int page = 1)
        {
            var vm = new HomeIndexViewModel();
            var photos = _photoService.RetrieveAllPhotos();
            vm.Photos = PagingList.Create(photos,20,page);
            
            return View(vm);
        }
    }
}