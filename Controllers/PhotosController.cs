using System;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IFileProcessor _fileProcessor;
        private readonly IPhotoService _photoServeice;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILikeService _likeService;

        public PhotosController(IFileProcessor fileProcessor,
                                IHttpContextAccessor httpContextAccessor,
                                IPhotoService photoService,
                                ILikeService likeService)
        {
            this._fileProcessor = fileProcessor;
            this._photoServeice = photoService;
            this._httpContextAccessor = httpContextAccessor;
            this._likeService = likeService;
        }
        
        [AllowAnonymous]
        public IActionResult ViewPhoto(int id)
        {
            var photo = _photoServeice.GetPhotoById(id);
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vm = new ViewPhotoViewModel()
            {
                Id = photo.Id,
                Title = photo.Title,
                Path = photo.Path,
                Views = _photoServeice.IncreasePhotoViews(id),
                UploaderId = photo.UploaderId,
                DateUploaded = photo.DateUploaded,
                DoesUserLikeThePhoto = _likeService.DoesUserLikeThePhoto(userId,id),
                Likes = _likeService.GetLikesCount(id)
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(UploadPhotoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string photoFileName = "";

                if (vm.Photo != null)
                   photoFileName = _fileProcessor.SavePhoto(vm.Photo,"Photos");

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                var photo = new Photo(){
                    Title = vm.Title,
                    Path = photoFileName,
                    UploaderId = userId,
                    DateUploaded = DateTime.Now
                };

                _photoServeice.SavePhoto(photo);
                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }
    }
}