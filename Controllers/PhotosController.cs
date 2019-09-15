using System;
using System.IO;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;


namespace _50Pixels.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public PhotosController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
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
            if(ModelState.IsValid)
            {   
                string uniqueFileName = "";

                if(vm.Photo != null)
                {
                    string uploadsFoler = Path.Combine(_hostingEnvironment.WebRootPath,"Photos");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;
                    string filePath = Path.Combine(uploadsFoler,uniqueFileName);
                    vm.Photo.CopyTo(new FileStream(filePath,FileMode.Create));
                }
                return RedirectToAction("Index","Home");
            }
            
            ViewBag.Title = "Upload Photo";
            return View();
            
        }
    }
}