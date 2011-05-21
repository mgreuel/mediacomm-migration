#region Using Directives

using System;
using System.Data;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data.NHInfrastructure;

using NHibernate;

#endregion

namespace MediaCommMVC.Web.Core.Data.Repositories
{
    using MediaCommMVC.Web.Core.Infrastructure;

    public class RepositoryBase
    {
        #region Constants and Fields

        private readonly ISessionContainer sessionManager;

        #endregion

        #region Constructors and Destructors

        public RepositoryBase(ISessionContainer sessionManager, IConfigAccessor configAccessor, ILogger logger)
        {
            this.sessionManager = sessionManager;
            this.ConfigAccessor = configAccessor;
            this.Logger = logger;
        }

        #endregion

        #region Properties

        protected IConfigAccessor ConfigAccessor { get; private set; }

        protected ILogger Logger { get; private set; }

        protected ISession Session
        {
            get
            {
                return this.sessionManager.CurrentSession;
            }
        }

        #endregion
    }
}