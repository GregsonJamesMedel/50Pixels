using System.Collections.Generic;
using _50Pixels.Data;
using _50Pixels.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _50Pixels.Services
{
    public class PhotoRepository : IPhotoService
    {
        private readonly AppDbContext _context;

        public PhotoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public bool DeletePhoto(int Id)
        {
            var photo = GetPhotoById(Id);
            _context.Photos.Remove(photo);
            _context.SaveChanges();
            return GetPhotoById(Id) != null ? false : true;
        }

        public Photo GetPhotoById(int id)
        {
            return _context.Photos.FirstOrDefault(photo => photo.Id == id);
        }

        public IEnumerable<Photo> GetPhotosByUploaderId(string id)
        {
            return _context.Photos.AsNoTracking().Where(p => p.UploaderId == id).OrderByDescending(p => p.DateUploaded);
        }

        public int IncreasePhotoViews(int Id)
        {
            var photo = GetPhotoById(Id);
            photo.Views = ++photo.Views;
            _context.SaveChanges();
            return photo.Views;
        }

        public IEnumerable<Photo> RetrieveAllPhotos()
        {
            return _context.Photos.AsNoTracking().OrderByDescending(p => p.DateUploaded);
        }

        public void SavePhoto(Photo photo)
        {
            this._context.Photos.Add(photo);
            this._context.SaveChanges();
        }

        public IEnumerable<Photo> SearchPhotoByTitle(string title)
        {
            return this._context.Photos.Where(photo => photo.Title.Contains(title));
        }

        public IEnumerable<Photo> GetLikedPhotos(string userId)
        {
            var res = from photo in _context.Photos
                        join like in _context.Likes
                        on photo.Id equals like.PhotoId
                        where like.LikerId == userId
                        select photo;

            return res;
        }
    }
}