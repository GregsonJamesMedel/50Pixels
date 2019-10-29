namespace _50Pixels.Services
{
    public interface IFollowService
    {
        void FollowUser(string userId);
        bool CheckIfFollower(string userId);
    }
}