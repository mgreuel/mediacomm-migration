using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Common.Config;
using System.Drawing.Imaging;

namespace MediaCommMVC.Data
{
    /// <summary>
    /// Uses .NET framework for generating small images and an external tool to generate medium and large images.
    /// </summary>
    public class MixedImageGenerator : IImageGenerator
    {
        /// <summary>
        /// The maximum height for thumbnail images.
        /// </summary>
        private const int MaxThumbnailHeight = 175;

        /// <summary>
        /// The maximum width for thumbnail images.
        /// </summary>
        private const int MaxThumbnailWidth = 175;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The config accessor.
        /// </summary>
        private readonly IConfigAccessor configAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MixedImageGenerator"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configAccessor">The config accessor.</param>
        public MixedImageGenerator(ILogger logger, IConfigAccessor configAccessor)
        {
            this.logger = logger;
            this.configAccessor = configAccessor;
        }

        /// <summary>
        /// Generates differnt resolution for the images.
        /// </summary>
        /// <param name="pathToPhotos">The path to photos.</param>
        /// <param name="unprocessedPhotosFolder">The unprocessed photos folder.</param>
        [System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.InheritanceDemand, Name = "FullTrust")]
        [System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Name = "FullTrust")]
        public void GenerateImages(string pathToPhotos, string unprocessedPhotosFolder)
        {
            this.logger.Debug("Generating smaller resolutions for photos in '{0}'", pathToPhotos);

            string sourcePath = Path.Combine(pathToPhotos, unprocessedPhotosFolder);

            this.GenerateSmallImages(pathToPhotos, sourcePath);
            this.GenerateMediumAndLargeImages(pathToPhotos, sourcePath);
        }

        /// <summary>
        /// Generates the small images.
        /// </summary>
        /// <param name="targetPath">The target path.</param>
        /// <param name="sourcePath">The source path.</param>
        private void GenerateSmallImages(string targetPath, string sourcePath)
        {
            IEnumerable<FileInfo> originalImages = new DirectoryInfo(sourcePath).GetFiles();

            foreach (FileInfo originalFile in originalImages)
            {
                using (Bitmap originalImage = new Bitmap(originalFile.FullName))
                {
                    using (Bitmap thumbnailImage = this.GetThumbnail(originalImage))
                    {
                        string thumbFilename = string.Format(
                            "{0}small{1}", originalFile.Name.Replace(originalFile.Extension, string.Empty), originalFile.Extension);
                        thumbnailImage.Save(Path.Combine(targetPath, thumbFilename), ImageFormat.Jpeg);
                    }
                }
            }
        }

        /// <summary>
        /// Generates the medium and large images.
        /// </summary>
        /// <param name="targetPath">The target path.</param>
        /// <param name="sourcePath">The source path.</param>
        private void GenerateMediumAndLargeImages(string targetPath, string sourcePath)
        {
            sourcePath = string.Format("{0}\\*", sourcePath.TrimEnd('\\'));
            targetPath = string.Format("{0}\\", targetPath.TrimEnd('\\'));

            string param = sourcePath + " " + targetPath;

            string photoCreatorBatchPath = this.configAccessor.GetConfigValue("PathPhotoCreatorBatch");

            this.logger.Debug("Executing '{0}' with parameters '{1}'", photoCreatorBatchPath, param);

            Process.Start(photoCreatorBatchPath, param);
        }

        /// <summary>
        /// Gets a thumbnail of the specified image.
        /// </summary>
        /// <param name="bmp">The original image.</param>
        /// <returns>The thumbnail for the specified image.</returns>
        private Bitmap GetThumbnail(Bitmap bmp)
        {
            float maxH = Convert.ToSingle(MaxThumbnailHeight);
            float maxW = Convert.ToSingle(MaxThumbnailWidth);
            float height = Convert.ToSingle(bmp.Height);
            float width = Convert.ToSingle(bmp.Width);

            float scale = Math.Max(height / maxH, width / maxW);
            int h = Convert.ToInt32(height / scale);
            int w = Convert.ToInt32(width / scale);

            Bitmap temp = new Bitmap(bmp.GetThumbnailImage(w, h, null, IntPtr.Zero));

            return temp;
        }
    }
}
