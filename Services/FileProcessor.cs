using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace _50Pixels.Services
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileProcessor(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }

        public string ChangePhoto(string oldPhotoPath, IFormFile newPhoto)
        {
            if (oldPhotoPath != "no-photo.png")
            {
                string oldFilePath = Path.Combine(this._hostingEnvironment.WebRootPath, "ProfilePics", oldPhotoPath);
                System.IO.File.Delete(oldFilePath);
            }
            return SavePhoto(newPhoto, "ProfilePics");
        }

        public string SavePhoto(IFormFile photo, string location)
        {
            string uniqueFileName = "";
            string uploadsFoler = Path.Combine(this._hostingEnvironment.WebRootPath, location);
            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            string filePath = Path.Combine(uploadsFoler, uniqueFileName);
            photo.CopyTo(new FileStream(filePath, FileMode.Create));
            return uniqueFileName;
        }
    }
}