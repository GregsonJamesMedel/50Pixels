namespace _50Pixels.Services
{
    public interface IPhotoFileProcessor
    {
        IPhotoService PhotoService { get; }
        IFileProcessor FileProcessor { get; }
    }
}