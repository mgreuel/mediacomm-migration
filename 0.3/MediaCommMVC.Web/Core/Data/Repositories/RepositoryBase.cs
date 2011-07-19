using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Infrastructure;

using NHibernate;

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    public class RepositoryBase
    {
        private readonly ISessionContainer sessionManager;

        public RepositoryBase(ISessionContainer sessionManager, IConfigAccessor configAccessor, ILogger logger)
        {
            this.sessionManager = sessionManager;
            this.ConfigAccessor = configAccessor;
            this.Logger = logger;
        }

        protected IConfigAccessor ConfigAccessor { get; private set; }

        protected ILogger Logger { get; private set; }

        protected ISession Session
        {
            get
            {
                return this.sessionManager.CurrentSession;
            }
        }
    }
}