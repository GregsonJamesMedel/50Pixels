using _50Pixels.Data;
using _50Pixels.Models;
using System.Linq;

namespace _50Pixels.Services
{
    public class LikeRepository : ILikeService
    {
        private readonly AppDbContext _context;
        private readonly IUserSessionService _userSessionService;

        public LikeRepository(AppDbContext context,
                              IUserSessionService userSessionService)
        {
            this._context = context;
            this._userSessionService = userSessionService;
        }

        public bool DoesUserLikeThePhoto(string userId, int photoId)
        {
            var result = _context.Likes.FirstOrDefault(x => x.PhotoId == photoId && x.LikerId == userId);
            return result != null ? true : false;
        }

        public int GetLikesCount(int photoId)
        {
            var result = _context.Likes.Where(x => x.PhotoId == photoId);
            return result.Count();
        }

        public bool LikePhoto(int photoId)
        {
            var like = new Like(){
                PhotoId = photoId,
                LikerId = _userSessionService.GetCurrentUserID()
            };

            _context.Likes.Add(like);
            _context.SaveChanges();
            return DoesUserLikeThePhoto(_userSessionService.GetCurrentUserID(),photoId);
        }

        public bool UnlikePhoto(int photoId)
        {
            var like = _context.Likes.FirstOrDefault(x => x.PhotoId == photoId && x.LikerId == _userSessionService.GetCurrentUserID());
            _context.Likes.Remove(like);
            _context.SaveChanges();
            return DoesUserLikeThePhoto(_userSessionService.GetCurrentUserID(),photoId);
        }
    }
}