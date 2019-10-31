using System;
using _50Pixels.Models;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace _50Pixels.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly IPhotoService _photoServeice;
        private readonly ILikeService _likeService;
        private readonly IUserSessionService _userSessionService;

        public PhotosController(IFileProcessor fileProcessor,
                                IUserSessionService userSessionService,
                                IPhotoService photoService,
                                ILikeService likeService)
        {
            this._fileProcessor = fileProcessor;
            this._photoServeice = photoService;
            this._userSessionService = userSessionService;
            this._likeService = likeService;
        }

        [AllowAnonymous]
        public IActionResult ViewPhoto(int id)
        {
            var photo = this._photoServeice.GetPhotoById(id);

            var vm = new ViewPhotoViewModel()
            {
                Id = photo.Id,
                Title = photo.Title,
                Path = photo.Path,
                Views = this._photoServeice.IncreasePhotoViews(id),
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
                    photoFileName = this._fileProcessor.SavePhoto(vm.Photo, "Photos"); //save photo in the folder

                var photo = new Photo()
                {
                    Title = vm.Title,
                    Path = photoFileName,
                    UploaderId = this._userSessionService.GetCurrentUserID(),
                    DateUploaded = DateTime.Now
                };

                this._photoServeice.SavePhoto(photo); //save photo path in the database
                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }

        public IActionResult DeletePhoto(int id)
        {
            if(this._photoServeice.DeletePhoto(id))
                this._likeService.DeleteLikes(id);
            
            return RedirectToAction("Index","Home");
        }

        [AllowAnonymous]
        public IActionResult Trending()
        {
            var vm = new PhotosTrendingViewModel();

            vm.Photos = this._photoServeice.RetrieveAllPhotos()
                        .Where(p => p.Views > 10)
                        .OrderByDescending(p => p.Views)
                        .Take(10);

            return View(vm);
        }

        [HttpGet]
        public IActionResult EditPhoto(int id)
        {
            var photo = this._photoServeice.GetPhotoById(id);

            var model = new PhotosEditPhotoVM();
            model.Id = photo.Id;
            model.Title = photo.Title;
            model.PhotoPath = photo.Path;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditPhoto(PhotosEditPhotoVM vm)
        {
            if(ModelState.IsValid)
            {
                var updatePhoto = new Photo();
                updatePhoto.Id = vm.Id;
                updatePhoto.Title = vm.Title;

                if(this._photoServeice.EditPhoto(updatePhoto))
                {
                    return RedirectToAction("ViewPhoto",new { id = updatePhoto.Id });
                }
            }

            return View(vm);
        }

    }
}