﻿#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Permissions;
using System.Threading;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;

#endregion

namespace MediaCommMVC.Web.Core.Data
{
    public class MixedImageGenerator : IImageGenerator
    {
        #region Constants and Fields

        private const int MaxThumbnailHeight = 175;

        private const int MaxThumbnailWidth = 175;

        private readonly IConfigAccessor configAccessor;

        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        public MixedImageGenerator(ILogger logger, IConfigAccessor configAccessor)
        {
            this.logger = logger;
            this.configAccessor = configAccessor;
        }

        #endregion

        #region Implemented Interfaces

        #region IImageGenerator

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

        private void QueueImageGeneration(object pathsTupel)
        {
            Tuple<string, string> paths = (Tuple<string, string>)pathsTupel;

            GenerateSmallImages(pathsTupel);
            this.GenerateMediumAndLargeImages(paths.Item1, paths.Item2);
        }

        #endregion
    }
}