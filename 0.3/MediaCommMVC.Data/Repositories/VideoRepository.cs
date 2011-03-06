using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

using MediaCommMVC.Common;
using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Videos;
using MediaCommMVC.Data.NHInfrastructure;

using NHibernate.Linq;

namespace MediaCommMVC.Data.Repositories
{
    public class VideoRepository : RepositoryBase, IVideoRepository
    {
        private const string videoRootDirKey = "VideoRootDir";

        private const string IncomingvideosFolderName = "incoming";

        public VideoRepository(ISessionManager sessionManager, IConfigAccessor configAccessor, ILogger logger)
            : base(sessionManager, configAccessor, logger)
        {
        }

        #region Implementation of IVideoRepository

        public VideoCategory GetCategoryById(int id)
        {
            return this.Session.Get<VideoCategory>(id);
        }

        public IEnumerable<VideoCategory> GetAllCategories()
        {
            return this.Session.Query<VideoCategory>().ToList();
        }

        public IEnumerable<string> GetUnmappedThumbnailFiles()
        {
            string incomingVideoPath = this.GetIncomingVideosPath();

            return
                Directory.GetFiles(incomingVideoPath).Where(
                    f =>
                    f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)).Select(
                        f => f.Substring(f.LastIndexOf('\\') + 1)).ToList();
        }

        private string GetIncomingVideosPath()
        {
            string basePath = this.ConfigAccessor.GetConfigValue(videoRootDirKey);
            string incomingVideosPath = Path.Combine(basePath, IncomingvideosFolderName);

            if (!Directory.Exists(incomingVideosPath))
            {
                Directory.CreateDirectory(incomingVideosPath);
            }

            return incomingVideosPath;
        }

        public IEnumerable<string> GetUnmappedVideoFiles()
        {
            string incomingVideoPath = this.GetIncomingVideosPath();

            return Directory.GetFiles(incomingVideoPath, "*.webm").Select(f => f.Substring(f.LastIndexOf('\\') + 1)).ToList();
        }

        public void AddCategory(VideoCategory videoCategory)
        {
            this.InvokeTransaction(s => s.Save(videoCategory));
        }

        public void AddVideo(Video video)
        {
            this.MoveVideoFiles(video);

            this.InvokeTransaction(s => s.Save(video));
        }

        public Image GetThumbnailImage(int videoId)
        {
            Video video = this.Session.Get<Video>(videoId);

            string basePath = this.ConfigAccessor.GetConfigValue(videoRootDirKey);
            string thumbnailFilename = Path.Combine(basePath, video.VideoCategory.Name, video.ThumbnailFileName);

            return Image.FromFile(thumbnailFilename);
        }

        public Video GetVideoById(int id)
        {
            return this.Session.Get<Video>(id);
        }

        public IEnumerable<string> GetUnmappedPosterFiles()
        {
            string incomingVideoPath = this.GetIncomingVideosPath();

            return
                Directory.GetFiles(incomingVideoPath).Where(
                    f =>
                    f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)).Select(
                        f => f.Substring(f.LastIndexOf('\\') + 1)).ToList();
        }

        private void MoveVideoFiles(Video video)
        {
            string basePath = this.ConfigAccessor.GetConfigValue(videoRootDirKey);
            string incomingVideosPath = this.GetIncomingVideosPath();

            string targetPath = Path.Combine(basePath, video.VideoCategory.Name);

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            string urlEncodedVideoFileName = UrlStripper.RemoveIllegalCharactersFromUrl(video.VideoFileName);
            string urlEncodedThumbnailFileName = UrlStripper.RemoveIllegalCharactersFromUrl(video.ThumbnailFileName);
            string urlEncodedPosterFileName = UrlStripper.RemoveIllegalCharactersFromUrl(video.PosterFileName);

            File.Move(Path.Combine(incomingVideosPath, video.VideoFileName), Path.Combine(targetPath, urlEncodedVideoFileName));
            File.Move(Path.Combine(incomingVideosPath, video.ThumbnailFileName), Path.Combine(targetPath, urlEncodedThumbnailFileName));
            File.Move(Path.Combine(incomingVideosPath, video.PosterFileName), Path.Combine(targetPath, urlEncodedPosterFileName));

            video.VideoFileName = urlEncodedVideoFileName;
            video.ThumbnailFileName = urlEncodedThumbnailFileName;
            video.PosterFileName = urlEncodedPosterFileName;
        }

        #endregion
    }
}
