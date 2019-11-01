namespace _50Pixels.Services
{
    public class PhotoFIleProcessor : IPhotoFileProcessor
    {
        private readonly IPhotoService _photoService;
        private readonly IFileProcessor _fileProcessor;

        public IPhotoService PhotoService { get => this._photoService; }
        public IFileProcessor FileProcessor { get => this._fileProcessor; }

        public PhotoFIleProcessor(IPhotoService photoService, IFileProcessor fileProcessor)
        {
            this._photoService = photoService;
            this._fileProcessor = fileProcessor;
        }
    }
}