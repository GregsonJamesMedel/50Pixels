using _50Pixels.Data;
using _50Pixels.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace _50Pixels.Services
{
    public class LikeRepository : ILikeService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LikeRepository(AppDbContext context,
                              IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
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
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var like = new Like(){
                PhotoId = photoId,
                LikerId = userId
            };

            _context.Likes.Add(like);
            _context.SaveChanges();
            return DoesUserLikeThePhoto(userId,photoId);
        }

        public bool UnlikePhoto(int photoId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var like = _context.Likes.FirstOrDefault(x => x.PhotoId == photoId && x.LikerId == userId);
            _context.Likes.Remove(like);
            _context.SaveChanges();
            return DoesUserLikeThePhoto(userId,photoId);
        }
    }
}