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