using System.Collections.Generic;
using _50Pixels.Models;

namespace _50Pixels.Services
{
    public interface IPhotoService
    {
        void SavePhoto(Photo photo);
        Photo GetPhotoById(int id);
        int IncreasePhotoViews(int Id);
        IEnumerable<Photo> RetrieveAllPhotos();
    }
}