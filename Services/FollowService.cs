using System.Collections.Generic;
using System.Linq;
using _50Pixels.Data;
using _50Pixels.Models;
using Microsoft.AspNetCore.Identity;

namespace _50Pixels.Services
{
    public class FollowService : IFollowService
    {
        private readonly AppDbContext _context;
        private readonly IUserSessionService _userSessionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FollowService(AppDbContext context,
                            IUserSessionService userSessionService,
                            UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userSessionService = userSessionService;
            this._userManager = userManager;
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
        public IEnumerable<ApplicationUser> GetFollowing(string userId)
        {
            return from appUser in this._userManager.Users
                   join following in this._context.Follows
                   on appUser.Id equals following.Following
                   where following.Follower == userId
                   select appUser;
        }

        public IEnumerable<ApplicationUser> GetFollowers(string userId)
        {
            return from appUser in this._userManager.Users
                   join followers in this._context.Follows
                   on appUser.Id equals followers.Follower
                   where followers.Following == userId
                   select appUser;

        }
    }
}