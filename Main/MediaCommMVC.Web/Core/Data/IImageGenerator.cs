namespace MediaCommMVC.Web.Core.Data
{
    public interface IImageGenerator
    {
        void GenerateImages(string pathToPhotos, string unprocessedPhotosFolder);
    }
}