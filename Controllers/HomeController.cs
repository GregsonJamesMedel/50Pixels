using System.Collections.Generic;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _50Pixels.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoService _photoService;

        public HomeController(IPhotoService photoService)
        {
            this._photoService = photoService;
        }
        
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Photos = _photoService.RetrieveAllPhotos();
            
            return View(model);
        }
    }
}