using System;
using System.Collections.Generic;
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

        #endregion
    }
}
