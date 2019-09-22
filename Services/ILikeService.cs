namespace _50Pixels.Services
{
    public interface ILikeService
    {
        void LikePhoto(int photoId);
        void UnlikePhoto(int photoId);
        bool DoesUserLikeThePhoto(string userId, int photoId);
        int GetLikesCount(int photoId);
    }
}