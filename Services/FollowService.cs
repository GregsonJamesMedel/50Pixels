using _50Pixels.Data;
using _50Pixels.Models;

namespace _50Pixels.Services
{
    public class FollowService : IFollowService
    {
        private readonly AppDbContext _context;
        private readonly IUserSessionService _userSessionService;

        public FollowService(AppDbContext context, IUserSessionService userSessionService)
        {
            this._context = context;
            this._userSessionService = userSessionService;
        }
        public void FollowUser(string userId)
        {
            var follow = new Follow()
            {
                Following = userId,
                Follower = this._userSessionService.GetCurrentUserID()
            };

            this._context.Follows.Add(follow);
        }
    }
}