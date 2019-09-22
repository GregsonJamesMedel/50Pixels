using System;
using System.IO;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using _50Pixels.Services;
using _50Pixels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace _50Pixels.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IPhotoService _photoServeice;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhotosController(IHostingEnvironment hostingEnvironment,
                                IHttpContextAccessor httpContextAccessor,
                                IPhotoService photoService)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._photoServeice = photoService;
            this._httpContextAccessor = httpContextAccessor;
        }
        [AllowAnonymous]
        public IActionResult ViewPhoto(int id)
        {
            var photo = _photoServeice.GetPhotoById(id);
            var vm = new ViewPhotoViewModel()
            {
                Id = photo.Id,
                Title = photo.Title,
                Path = photo.Path
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            ViewBag.Title = "Upload Photo";
            return View();
        }

        [HttpPost]
        public IActionResult Upload(UploadPhotoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = "";

                if (vm.Photo != null)
                {
                    string uploadsFoler = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;
                    string filePath = Path.Combine(uploadsFoler, uniqueFileName);
                    vm.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                var photo = new Photo(){
                    Title = vm.Title,
                    Path = uniqueFileName,
                    UploaderId = userId,
                    DateUploaded = DateTime.Now
                };

                _photoServeice.SavePhoto(photo);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Upload Photo";
            return View(vm);

        }
    }
}