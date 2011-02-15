namespace MediaCommMVC.Data
{
    /// <summary>Common interface for image generation.</summary>
    public interface IImageGenerator
    {
        #region Public Methods

        /// <summary>Generates different resolutions for the images.</summary>
        /// <param name="pathToPhotos">The path to photos.</param>
        /// <param name="unprocessedPhotosFolder">The unprocessed photos folder.</param>
        void GenerateImages(string pathToPhotos, string unprocessedPhotosFolder);

        #endregion
    }
}
