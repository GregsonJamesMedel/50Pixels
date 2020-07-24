using Microsoft.AspNetCore.Http;

namespace _50Pixels.Services
{
    public interface IFileProcessor
    {
        string SavePhoto(IFormFile photo,string location);
        void DeletePhoto(string path, string location);
        string ChangePhoto(string oldPhotoPath,IFormFile newPhoto);
    }
}