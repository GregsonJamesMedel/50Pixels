using System.Collections.Generic;
using _50Pixels.Data;
using _50Pixels.Models;
using System.Linq;

namespace _50Pixels.Services
{
    public class PhotoRepository : IPhotoService
    {
        private readonly AppDbContext _context;

        public PhotoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public Photo GetPhotoById(int id)
        {
            return _context.Photos.FirstOrDefault(photo => photo.Id == id);
        }

        public IEnumerable<Photo> GetPhotosByUploaderId(string id)
        {
           return _context.Photos.Where(p => p.UploaderId == id);
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
            return _context.Photos;
        }

        public void SavePhoto(Photo photo)
        {
            this._context.Photos.Add(photo);
            this._context.SaveChanges();
        }
    }
}