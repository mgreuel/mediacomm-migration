#region Using Directives

using System;
using System.Data;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Data.NHInfrastructure;

using NHibernate;

#endregion

namespace MediaCommMVC.Data.Repositories
{
    /// <summary>Repository base class, handling NHibernate sessions and transactions.</summary>
    public class RepositoryBase
    {
        #region Constants and Fields

        /// <summary>The NHibernate session manager.</summary>
        private readonly ISessionManager sessionManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="RepositoryBase"/> class.</summary>
        /// <param name="sessionManager">The NHibernate session manager.</param>
        /// <param name="configAccessor">The config Accessor.</param>
        /// <param name="logger">The logger.</param>
        public RepositoryBase(ISessionManager sessionManager, IConfigAccessor configAccessor, ILogger logger)
        {
            this.sessionManager = sessionManager;
            this.ConfigAccessor = configAccessor;
            this.Logger = logger;
        }

        #endregion

        #region Properties

        /// <summary>Gets the config accessor.</summary>
        /// <value>The config accessor.</value>
        protected IConfigAccessor ConfigAccessor { get; private set; }

        /// <summary>Gets the loggger.</summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; private set; }

        /// <summary>Gets the NHibernate session.</summary>
        /// <value>The session.</value>
        protected ISession Session
        {
            get
            {
                return this.sessionManager.Session;
            }
        }

        #endregion

        #region Methods

        /// <summary>Invokes the action wihtin a nHibernate transaction.</summary>
        /// <param name="action">The action.</param>
        protected void InvokeTransaction(Action<ISession> action)
        {
            using (ITransaction transaction = this.Session.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    action(this.Session);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    throw;
                }
            }
        }

        #endregion
    }
}