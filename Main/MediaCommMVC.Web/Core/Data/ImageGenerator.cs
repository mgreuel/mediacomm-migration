﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediaCommMVC.Web.Core.Common.Logging;

namespace MediaCommMVC.Web.Core.Data
{
    public class ImageGenerator : IImageGenerator
    {
        private const long JpegQuality = 80;

        private const float MaxLargeHeight = 700;

        private const float MaxLargeWidth = 1050;

        private const float MaxMediumHeight = 500;

        private const float MaxMediumWidth = 750;

        private const float MaxThumbnailHeight = 175;

        private const float MaxThumbnailWidth = 175;

        private static readonly ImageCodecInfo JpegEncoder = ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

        private readonly ILogger logger;

        public ImageGenerator(ILogger logger)
        {
            this.logger = logger;
        }

        public void GenerateImages(string pathToPhotos, IEnumerable<FileInfo> sourceImages)
        {
            Tuple<string, IEnumerable<FileInfo>> paths = new Tuple<string, IEnumerable<FileInfo>>(pathToPhotos, sourceImages);

            Thread imageGenerationThread = new Thread(this.GenerateAllImages);
            imageGenerationThread.Start(paths);
        }

        private void GenerateAllImages(object pathsTupel)
        {
            try
            {
                Tuple<string, IEnumerable<FileInfo>> paths = (Tuple<string, IEnumerable<FileInfo>>)pathsTupel;

                IEnumerable<FileInfo> originalImages = paths.Item2;

                foreach (FileInfo originalFile in originalImages)
                {
                    using (Image originalImage = Image.FromFile(originalFile.FullName))
                    {
                        using (Image resizedImage = this.GetResizedImage(originalImage, MaxThumbnailWidth, MaxThumbnailHeight))
                        {
                            string targetFilename = string.Format(
                                "{0}small{1}", originalFile.Name.Replace(originalFile.Extension, string.Empty), originalFile.Extension);

                            string targetFilePath = Path.Combine(paths.Item1, targetFilename);
                            this.SaveResizedImage(resizedImage, targetFilePath);
                        }

                        using (Image resizedImage = this.GetResizedImage(originalImage, MaxMediumWidth, MaxMediumHeight))
                        {
                            string targetFilename = string.Format(
                                "{0}medium{1}", originalFile.Name.Replace(originalFile.Extension, string.Empty), originalFile.Extension);

                            string targetFilePath = Path.Combine(paths.Item1, targetFilename);
                            this.SaveResizedImage(resizedImage, targetFilePath);
                        }

                        using (Image resizedImage = this.GetResizedImage(originalImage, MaxLargeWidth, MaxLargeHeight))
                        {
                            string targetFilename = string.Format(
                                "{0}large{1}", originalFile.Name.Replace(originalFile.Extension, string.Empty), originalFile.Extension);

                            string targetFilePath = Path.Combine(paths.Item1, targetFilename);
                            this.SaveResizedImage(resizedImage, targetFilePath);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("Unable to generate images", exception);
            }
        }

        private Image GetResizedImage(Image originalImage, float maxWidth, float maxHeight)
        {
            float originalHeight = Convert.ToSingle(originalImage.Height);
            float originalWidth = Convert.ToSingle(originalImage.Width);

            float scale = Math.Max(originalHeight / maxHeight, originalWidth / maxWidth);
            int height = Convert.ToInt32(originalHeight / scale);
            int width = Convert.ToInt32(originalWidth / scale);

            Image resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(originalImage, 0, 0, width, height);
            }

            return resizedImage;
        }

        private void SaveResizedImage(Image resizedImage, string targetFilePath)
        {
            using (EncoderParameters encoderParameters = new EncoderParameters(1))
            {
                using (EncoderParameter qualityEncoderParameter = new EncoderParameter(Encoder.Quality, JpegQuality))
                {
                    encoderParameters.Param[0] = qualityEncoderParameter;
                    resizedImage.Save(targetFilePath, JpegEncoder, encoderParameters);
                }
            }
        }
    }
}