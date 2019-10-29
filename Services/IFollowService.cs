namespace _50Pixels.Services
{
    public interface IFollowService
    {
        bool FollowUser(string userId);
        bool UnFollowUser(string userId);
        bool CheckIfFollower(string userId);
    }
}