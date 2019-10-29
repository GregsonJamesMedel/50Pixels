using System.Collections.Generic;
using _50Pixels.Models;

namespace _50Pixels.Services
{
    public interface IFollowService
    {
        bool FollowUser(string userId);
        bool UnFollowUser(string userId);
        bool CheckIfFollower(string userId);
        IEnumerable<Follow> GetFollowing(string userId);
        IEnumerable<Follow> GetFollowers(string userId);
    }
}