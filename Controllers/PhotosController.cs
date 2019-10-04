using System;
using _50Pixels.Models;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using ReflectionIT.Mvc.Paging;

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
            var photo = _photoServeice.GetPhotoById(id);

            var vm = new ViewPhotoViewModel()
            {
                Id = photo.Id,
                Title = photo.Title,
                Path = photo.Path,
                Views = _photoServeice.IncreasePhotoViews(id),
                UploaderId = photo.UploaderId,
                DateUploaded = photo.DateUploaded,
                DoesUserLikeThePhoto = _likeService.DoesUserLikeThePhoto(_userSessionService.GetCurrentUserID(), id),
                Likes = _likeService.GetLikesCount(id),
                ViewerIsTheUploader = photo.UploaderId == _userSessionService.GetCurrentUserID()

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
                    photoFileName = _fileProcessor.SavePhoto(vm.Photo, "Photos");

                var photo = new Photo()
                {
                    Title = vm.Title,
                    Path = photoFileName,
                    UploaderId = _userSessionService.GetCurrentUserID(),
                    DateUploaded = DateTime.Now
                };

                _photoServeice.SavePhoto(photo);
                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }

        public IActionResult DeletePhoto(int id)
        {
            if(_photoServeice.DeletePhoto(id))
                _likeService.DeleteLikes(id);
            
            return RedirectToAction("Index","Home");
        }

        [AllowAnonymous]
        public IActionResult Trending()
        {
            var vm = new PhotosTrendingViewModel();
            var photos = _photoServeice.RetrieveAllPhotos().Where(p => p.Views > 10).Take(10);
            vm.Photos = PagingList.Create(photos,photos.Count(),0);
            return View(vm);
        }
    }
}