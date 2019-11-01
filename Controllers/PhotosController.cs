using System;
using System.Linq;
using _50Pixels.Models;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace _50Pixels.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private readonly ILikeService _likeService;
        private readonly IPhotoFileProcessor _photoFileProcessor;
        private readonly IUserSessionService _userSessionService;

        public PhotosController(IUserSessionService userSessionService,
                                IPhotoFileProcessor photoFileProcessor,
                                ILikeService likeService)
        {
            this._userSessionService = userSessionService;
            this._photoFileProcessor = photoFileProcessor;
            this._likeService = likeService;
        }

        [AllowAnonymous]
        public IActionResult ViewPhoto(int id)
        {
            var photo = this._photoFileProcessor.PhotoService.GetPhotoById(id);

            var vm = new ViewPhotoViewModel()
            {
                Id = photo.Id,
                Title = photo.Title,
                Path = photo.Path,
                Views = this._photoFileProcessor.PhotoService.IncreasePhotoViews(id),
                UploaderId = photo.UploaderId,
                DateUploaded = photo.DateUploaded,
                DoesUserLikeThePhoto = this._likeService.DoesUserLikeThePhoto(_userSessionService.GetCurrentUserID(), id),
                Likes = this._likeService.GetLikesCount(id),
                ViewerIsTheUploader = photo.UploaderId == this._userSessionService.GetCurrentUserID()

            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Upload() => View();

        [HttpPost]
        public IActionResult Upload(UploadPhotoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string photoFileName = "";

                if (vm.Photo != null)
                    photoFileName = this._photoFileProcessor.FileProcessor.SavePhoto(vm.Photo, "Photos"); //save photo in the folder

                var photo = new Photo()
                {
                    Title = vm.Title,
                    Path = photoFileName,
                    UploaderId = this._userSessionService.GetCurrentUserID(),
                    DateUploaded = DateTime.Now
                };

                this._photoFileProcessor.PhotoService.SavePhoto(photo); //save photo path in the database
                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }

        public IActionResult DeletePhoto(int id)
        {
            if (this._photoFileProcessor.PhotoService.DeletePhoto(id))
                this._likeService.DeleteLikes(id);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Trending()
        {
            var vm = new PhotosTrendingViewModel();

            vm.Photos = this._photoFileProcessor.PhotoService.RetrieveAllPhotos()
                        .Where(p => p.Views > 10)
                        .OrderByDescending(p => p.Views)
                        .Take(10);

            return View(vm);
        }

        [HttpGet]
        public IActionResult EditPhoto(int id)
        {
            var photo = this._photoFileProcessor.PhotoService.GetPhotoById(id);

            var model = new PhotosEditPhotoVM();
            model.Id = photo.Id;
            model.Title = photo.Title;
            model.PhotoPath = photo.Path;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditPhoto(PhotosEditPhotoVM vm)
        {
            if (ModelState.IsValid)
            {
                var updatePhoto = new Photo();
                updatePhoto.Id = vm.Id;
                updatePhoto.Title = vm.Title;

                if (this._photoFileProcessor.PhotoService.EditPhoto(updatePhoto))
                {
                    return RedirectToAction("ViewPhoto", new { id = updatePhoto.Id });
                }
            }

            return View(vm);
        }

    }
}