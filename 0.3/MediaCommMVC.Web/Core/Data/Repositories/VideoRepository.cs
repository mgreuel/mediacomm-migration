using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using MediaCommMVC.Web.Core.Common;
using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Infrastructure;
using MediaCommMVC.Web.Core.Model.Videos;

using NHibernate;
using NHibernate.Linq;

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private const string IncomingvideosFolderName = "incoming";

        private const string VideoRootDirKey = "VideoRootDir";

        private readonly IConfigAccessor configAccessor;

        private readonly ISessionContainer sessionContainer;

        public VideoRepository(ISessionContainer sessionContainer, IConfigAccessor configAccessor)
        {
            this.sessionContainer = sessionContainer;
            this.configAccessor = configAccessor;
        }

        protected ISession Session
        {
            get
            {
                return this.sessionContainer.CurrentSession;
            }
        }

        public void AddCategory(VideoCategory videoCategory)
        {
            this.Session.Save(videoCategory);
        }

        public void AddVideo(Video video)
        {
            this.MoveVideoFiles(video);

            this.Session.Save(video);
        }

        public IEnumerable<VideoCategory> GetAllCategories()
        {
            return this.Session.Query<VideoCategory>().ToList();
        }

        public VideoCategory GetCategoryById(int id)
        {
            return this.Session.Get<VideoCategory>(id);
        }

        public Image GetThumbnailImage(int videoId)
        {
            Video video = this.Session.Get<Video>(videoId);

            string basePath = this.configAccessor.GetConfigValue(VideoRootDirKey);
            string thumbnailFilename = Path.Combine(basePath, video.VideoCategory.Name, video.ThumbnailFileName);

            return Image.FromFile(thumbnailFilename);
        }

        public IEnumerable<string> GetUnmappedPosterFiles()
        {
            string incomingVideoPath = this.GetIncomingVideosPath();

            return
                Directory.GetFiles(incomingVideoPath).Where(
                    f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)).Select(
                        f => f.Substring(f.LastIndexOf('\\') + 1)).ToList();
        }

        public IEnumerable<string> GetUnmappedThumbnailFiles()
        {
            string incomingVideoPath = this.GetIncomingVideosPath();

            return
                Directory.GetFiles(incomingVideoPath).Where(
                    f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)).Select(
                        f => f.Substring(f.LastIndexOf('\\') + 1)).ToList();
        }

        public IEnumerable<string> GetUnmappedVideoFiles()
        {
            string incomingVideoPath = this.GetIncomingVideosPath();

            return Directory.GetFiles(incomingVideoPath, "*.webm").Select(f => f.Substring(f.LastIndexOf('\\') + 1)).ToList();
        }

        public Video GetVideoById(int id)
        {
            return this.Session.Get<Video>(id);
        }

        private string GetIncomingVideosPath()
        {
            string basePath = this.configAccessor.GetConfigValue(VideoRootDirKey);
            string incomingVideosPath = Path.Combine(basePath, IncomingvideosFolderName);

            if (!Directory.Exists(incomingVideosPath))
            {
                Directory.CreateDirectory(incomingVideosPath);
            }

            return incomingVideosPath;
        }

        private void MoveVideoFiles(Video video)
        {
            string basePath = this.configAccessor.GetConfigValue(VideoRootDirKey);
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
    }
}