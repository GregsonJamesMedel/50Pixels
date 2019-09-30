using _50Pixels.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    [Authorize]
    public class ReactionController : Controller
    {
        private readonly ILikeService _likeService;

        public ReactionController(ILikeService likeService)
        {
            this._likeService = likeService;
        }

        public IActionResult Like(int id)
        {
            var doesUserLikeThePhoto = _likeService.LikePhoto(id);
            return RedirectToAction("ViewPhoto", "Photos", new { id = id });
        }

        public IActionResult Unlike(int id)
        {
            _likeService.UnlikePhoto(id);
            return RedirectToAction("ViewPhoto", "Photos", new { id = id });
        }

    }
}