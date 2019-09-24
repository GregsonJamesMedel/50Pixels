namespace _50Pixels.Services
{
    public interface ILikeService
    {
        bool LikePhoto(int photoId);
        bool UnlikePhoto(int photoId);
        bool DoesUserLikeThePhoto(string userId, int photoId);
        int GetLikesCount(int photoId);
    }
}