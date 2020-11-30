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
            var userId = this._userSessionService.GetCurrentUserID();

            if (photo != null)
            {
                var vm = new ViewPhotoViewModel()
                {
                    Id = photo.Id,
                    Title = photo.Title,
                    Path = photo.Path,
                    Views = this._photoFileProcessor.PhotoService.IncreasePhotoViews(id),
                    UploaderId = photo.UploaderId,
                    DateUploaded = photo.DateUploaded,
                    DoesUserLikeThePhoto = this._likeService.DoesUserLikeThePhoto(userId, id),
                    Likes = this._likeService.GetLikesCount(id),
                    ViewerIsTheUploader = photo.UploaderId == userId

                };
                return View(vm);
            }

            return RedirectToAction("ErrorNotFound", "Error");

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
            var photo = this._photoFileProcessor.PhotoService.GetPhotoById(id);

            if (photo != null)
            {

                if (this._photoFileProcessor.PhotoService.DeletePhoto(id))
                    this._likeService.DeleteLikes(id);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("ErrorNotFound", "Error");
        }

        [AllowAnonymous]
        public IActionResult Trending()
        {
            var vm = new PhotosTrendingViewModel();

            vm.Photos = this._photoFileProcessor.PhotoService.RetrieveAllPhotos()
                        .Where(p => p.Views >= 5)
                        .OrderByDescending(p => p.Views);

            return View(vm);
        }

        [HttpGet]
        public IActionResult EditPhoto(int id)
        {
            var photo = this._photoFileProcessor.PhotoService.GetPhotoById(id);

            if (photo != null)
            {
                var model = new PhotosEditPhotoVM();
                model.Id = photo.Id;
                model.Title = photo.Title;
                model.PhotoPath = photo.Path;

                return View(model);
            }
            return RedirectToAction("ErrorNotFound", "Error");
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