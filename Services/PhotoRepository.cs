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
        private readonly IFileProcessor _fileProcessor;

        public PhotoRepository(AppDbContext context, IFileProcessor fileProcessor)
        {
            this._context = context;
            this._fileProcessor = fileProcessor;
        }

        public bool DeletePhoto(int Id)
        {
            var photo = GetPhotoById(Id);
            this._context.Photos.Remove(photo);
            this._context.SaveChanges();
            this._fileProcessor.DeletePhoto(photo.Path,"Photos");
            return GetPhotoById(Id) != null ? false : true;
        }

        public Photo GetPhotoById(int id)
        {
            return this._context.Photos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Photo> GetPhotosByUploaderId(string id)
        {
            return this._context.Photos.AsNoTracking().Where(p => p.UploaderId == id).OrderByDescending(p => p.DateUploaded);
        }

        public int IncreasePhotoViews(int Id)
        {
            var photo = GetPhotoById(Id);
            photo.Views = ++photo.Views;
            this._context.SaveChanges();
            return photo.Views;
        }

        public IEnumerable<Photo> RetrieveAllPhotos()
        {
            return this._context.Photos.AsNoTracking().OrderByDescending(p => p.DateUploaded);
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
            var res = from photo in this._context.Photos
                        join like in this._context.Likes
                        on photo.Id equals like.PhotoId
                        where like.LikerId == userId
                        select photo;

            return res;
        }

        public bool EditPhoto(Photo photo)
        {
            var OriginalPhoto = this._context.Photos.FirstOrDefault(p => p.Id == photo.Id);
            
            OriginalPhoto.Title = photo.Title;
            this._context.Photos.Update(OriginalPhoto);
            var result = this._context.SaveChanges();

            return result > 0 ? true : false;
        }

    }
}