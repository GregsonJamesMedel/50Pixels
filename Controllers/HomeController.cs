using System.Collections.Generic;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            var photos = new List<DisplayPhotoViewModel>()
            {
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/994883/pexels-photo-994883.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/2902541/pexels-photo-2902541.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/1629236/pexels-photo-1629236.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/2887620/pexels-photo-2887620.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/459203/pexels-photo-459203.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/205316/pexels-photo-205316.png?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/1537008/pexels-photo-1537008.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/2887618/pexels-photo-2887618.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/2887634/pexels-photo-2887634.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/1919287/pexels-photo-1919287.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/2886054/pexels-photo-2886054.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/2893177/pexels-photo-2893177.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/584399/living-room-couch-interior-room-584399.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/313690/pexels-photo-313690.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/1149137/pexels-photo-1149137.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/2287310/pexels-photo-2287310.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" },
                new DisplayPhotoViewModel(){ Path="https://images.pexels.com/photos/2900402/pexels-photo-2900402.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" }
            };

            var model = new HomeIndexViewModel();
            model.Photos = photos;
            ViewBag.Title = "50 Pixels: Index";
            return View(model);
        }
    }
}