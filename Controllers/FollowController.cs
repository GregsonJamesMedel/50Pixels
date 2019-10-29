using _50Pixels.Services;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class FollowController : Controller
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            this._followService = followService;
        }

        public IActionResult FollowUser(string id)
        {
            this._followService.FollowUser(id);
            return RedirectToAction("Profile","Account",new { id = id });
        }
    }
}