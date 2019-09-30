using System;
using _50Pixels.Models;

namespace _50Pixels.ViewModels
{
    public class ViewPhotoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int Views { get; set; }
        public bool DoesUserLikeThePhoto { get; set; }
        public int Likes { get; set; }
        public DateTime DateUploaded { get; set; }
        public string UploaderId { get; set; }
        public bool ViewerIsTheUploader { get; set; }
    }
}