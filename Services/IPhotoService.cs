using System.Collections.Generic;
using _50Pixels.Models;

namespace _50Pixels.Services
{
    public interface IPhotoService
    {
        void SavePhoto(Photo photo);
        IEnumerable<Photo> RetrieveAllPhotos();
        Photo GetPhotoById(int id);
    }
}