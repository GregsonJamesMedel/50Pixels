using System.Collections.Generic;
using System.Linq;
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

        public bool CheckIfFollower(string userId)
        {
            var currentUser = this._userSessionService.GetCurrentUserID();
            var result = this._context.Follows.FirstOrDefault(f => f.Following == userId && f.Follower == currentUser);
            return result != null;
        }

        public bool FollowUser(string userId)
        {
            var follow = new Follow();
            follow.Following = userId;
            follow.Follower = this._userSessionService.GetCurrentUserID();

            this._context.Follows.Add(follow);
            this._context.SaveChanges();

            return CheckIfFollower(userId);
        }


        public bool UnFollowUser(string userId)
        {
            var currentUser = this._userSessionService.GetCurrentUserID();
            var unfollow = this._context.Follows.FirstOrDefault(f => f.Following == userId && f.Follower == currentUser);
            
            this._context.Follows.Remove(unfollow);
            this._context.SaveChanges();
            
            return CheckIfFollower(userId);
        }
        public IEnumerable<Follow> GetFollowing(string userId)
        {
            return this._context.Follows.Where(f => f.Follower == userId);
        }
    }
}