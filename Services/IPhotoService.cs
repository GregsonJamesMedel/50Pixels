using System.Collections.Generic;
using _50Pixels.Models;

namespace _50Pixels.Services
{
    public interface IPhotoService
    {
        void SavePhoto(Photo photo);
        Photo GetPhotoById(int id);
        int IncreasePhotoViews(int Id);
        bool DeletePhoto(int Id);
        IEnumerable<Photo> GetPhotosByUploaderId(string id);
        IEnumerable<Photo> RetrieveAllPhotos();
        IEnumerable<Photo> SearchPhotoByTitle(string title);
        IEnumerable<Photo> GetLikedPhotos(string userId);
    }
}