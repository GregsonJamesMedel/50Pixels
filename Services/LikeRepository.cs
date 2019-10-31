using _50Pixels.Data;
using _50Pixels.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public void DeleteLikes(int photoId)
        {
           var result = this._context.Likes.Where(l => l.PhotoId == photoId);
           this._context.Likes.RemoveRange(result);
           this._context.SaveChanges();
        }

        public bool DoesUserLikeThePhoto(string userId, int photoId)
        {
            var result = this._context.Likes.FirstOrDefault(x => x.PhotoId == photoId && x.LikerId == userId);
            return result != null ? true : false;
        }

        public int GetLikesCount(int photoId)
        {
            var result = this._context.Likes.Where(x => x.PhotoId == photoId);
            return result.Count();
        }

        public bool LikePhoto(int photoId)
        {
            var like = new Like(){
                PhotoId = photoId,
                LikerId = this._userSessionService.GetCurrentUserID()
            };

            this._context.Likes.Add(like);
            this._context.SaveChanges();
            return DoesUserLikeThePhoto(this._userSessionService.GetCurrentUserID(),photoId);
        }

        public bool UnlikePhoto(int photoId)
        {
            var like = this._context.Likes.FirstOrDefault(x => x.PhotoId == photoId && x.LikerId == _userSessionService.GetCurrentUserID());
            this._context.Likes.Remove(like);
            this._context.SaveChanges();
            return DoesUserLikeThePhoto(this._userSessionService.GetCurrentUserID(),photoId);
        }
    }
}