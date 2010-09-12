using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Data
{
    /// <summary>
    /// Common interface for image generation.
    /// </summary>
    public interface IImageGenerator
    {
        /// <summary>
        /// Generates different resolutions for the images.
        /// </summary>
        /// <param name="pathToPhotos">The path to photos.</param>
        /// <param name="unprocessedPhotosFolder">The unprocessed photos folder.</param>
        void GenerateImages(string pathToPhotos, string unprocessedPhotosFolder);
    }
}
