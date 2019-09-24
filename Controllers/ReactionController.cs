using System.Security.Claims;
using _50Pixels.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class ReactionController : Controller
    {
        private readonly ILikeService _likeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReactionController(ILikeService likeService, IHttpContextAccessor httpContextAccessor)
        {
            this._likeService = likeService;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Like(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var doesUserLikeThePhoto = _likeService.LikePhoto(id);
                return RedirectToAction("ViewPhoto", "Photos", new { id = id });
            }
            return RedirectToAction("SignIn", "Account");
        }

        public IActionResult Unlike(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                _likeService.UnlikePhoto(id);
                return RedirectToAction("ViewPhoto", "Photos", new { id = id });
            }
            return RedirectToAction("SignIn", "Account");
        }

    }
}