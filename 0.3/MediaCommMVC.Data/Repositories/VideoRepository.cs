using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            return Directory.GetFiles(incomingVideoPath).Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private string GetIncomingVideosPath()
        {
            string basePath = this.ConfigAccessor.GetConfigValue("VideoRootDir");
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

            return Directory.GetFiles(incomingVideoPath, ".webm");
        }

        #endregion
    }
}
