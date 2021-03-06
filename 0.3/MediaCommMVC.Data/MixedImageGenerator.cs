﻿#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Permissions;
using System.Threading;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;

#endregion

namespace MediaCommMVC.Data
{
    /// <summary>Uses .NET framework for generating small images and an external tool to generate medium and large images.</summary>
    public class MixedImageGenerator : IImageGenerator
    {
        #region Constants and Fields

        /// <summary>The maximum height for thumbnail images.</summary>
        private const int MaxThumbnailHeight = 175;

        /// <summary>The maximum width for thumbnail images.</summary>
        private const int MaxThumbnailWidth = 175;

        /// <summary>The config accessor.</summary>
        private readonly IConfigAccessor configAccessor;

        /// <summary>The logger.</summary>
        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="MixedImageGenerator"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configAccessor">The config accessor.</param>
        public MixedImageGenerator(ILogger logger, IConfigAccessor configAccessor)
        {
            this.logger = logger;
            this.configAccessor = configAccessor;
        }

        #endregion

        #region Implemented Interfaces

        #region IImageGenerator

        /// <summary>Generates differnt resolution for the images.</summary>
        /// <param name="pathToPhotos">The path to photos.</param>
        /// <param name="unprocessedPhotosFolder">The unprocessed photos folder.</param>
        [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public void GenerateImages(string pathToPhotos, string unprocessedPhotosFolder)
        {
            this.logger.Debug("Generating smaller resolutions for photos in '{0}'", pathToPhotos);

            string sourcePath = Path.Combine(pathToPhotos, unprocessedPhotosFolder);

            Tuple<string, string> paths = new Tuple<string, string>(pathToPhotos, sourcePath);
            ThreadPool.QueueUserWorkItem(this.QueueImageGeneration, paths);
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Generates the small images.
        /// </summary>
        /// <param name="pathsTupel">The paths tupel.</param>
        private static void GenerateSmallImages(object pathsTupel)
        {
            Tuple<string, string> paths = (Tuple<string, string>)pathsTupel;

            IEnumerable<FileInfo> originalImages = new DirectoryInfo(paths.Item2).GetFiles();

            foreach (FileInfo originalFile in originalImages)
            {
                using (Bitmap originalImage = new Bitmap(originalFile.FullName))
                {
                    using (Bitmap thumbnailImage = GetThumbnail(originalImage))
                    {
                        string thumbFilename = string.Format(
                            "{0}small{1}", originalFile.Name.Replace(originalFile.Extension, string.Empty), originalFile.Extension);
                        thumbnailImage.Save(Path.Combine(paths.Item1, thumbFilename), ImageFormat.Jpeg);
                    }
                }
            }
        }

        /// <summary>Gets a thumbnail of the specified image.</summary>
        /// <param name="bmp">The original image.</param>
        /// <returns>The thumbnail for the specified image.</returns>
        private static Bitmap GetThumbnail(Bitmap bmp)
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

        /// <summary>Generates the medium and large images.</summary>
        /// <param name="targetPath">The target path.</param>
        /// <param name="sourcePath">The source path.</param>
        private void GenerateMediumAndLargeImages(string targetPath, string sourcePath)
        {
            sourcePath = string.Format("{0}\\*", sourcePath.TrimEnd('\\'));
            targetPath = string.Format("{0}\\", targetPath.TrimEnd('\\'));

            string param = sourcePath + " " + targetPath;

            string photoCreatorBatchPath = this.configAccessor.GetConfigValue("PathPhotoCreatorBatch");

            this.logger.Debug("Executing '{0}' with parameters '{1}'", photoCreatorBatchPath, param);

            Process process = Process.Start(photoCreatorBatchPath, param);
            process.PriorityClass = ProcessPriorityClass.BelowNormal;
        }

        /// <summary>
        /// Queues the image generation.
        /// </summary>
        /// <param name="pathsTupel">The paths tupel.</param>
        private void QueueImageGeneration(object pathsTupel)
        {
            Tuple<string, string> paths = (Tuple<string, string>)pathsTupel;

            GenerateSmallImages(pathsTupel);
            this.GenerateMediumAndLargeImages(paths.Item1, paths.Item2);
        }

        #endregion
    }
}