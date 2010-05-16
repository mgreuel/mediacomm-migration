#region Using Directives

using System;
using System.Web;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Data.NHInfrastructure;
using MediaCommMVC.Data.NHInfrastructure.Config;

using NHibernate;
using NHibernate.Context;

#endregion

namespace MediaCommMVC.UI.Infrastructure
{
    /// <summary>Handles the NHibernate session management in an web application.</summary>
    public sealed class WebSessionManager : ISessionManager, IDisposable
    {
        #region Constants and Fields

        /// <summary>
        ///   The logger.
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="WebSessionManager"/> class.</summary>
        /// <param name="configurationGenerator">The configuration generator.</param>
        /// <param name="logger">The logger.</param>
        public WebSessionManager(IConfigurationGenerator configurationGenerator, ILogger logger)
        {
            this.SessionFactory = configurationGenerator.Generate().BuildSessionFactory();
            this.logger = logger;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets the NHibernate session.
        /// </summary>
        /// <value>The NHibernate session.</value>
        public ISession Session
        {
            get
            {
                this.logger.Debug("Getting session from HttpContext");
                if (!ManagedWebSessionContext.HasBind(HttpContext.Current, this.SessionFactory))
                {
                    this.logger.Debug("HttpContext has no session attached, creating and attaching new session");
                    ManagedWebSessionContext.Bind(HttpContext.Current, this.SessionFactory.OpenSession());
                }

                return this.SessionFactory.GetCurrentSession();
            }
        }

        /// <summary>
        ///   Gets the NHibernate session factory.
        /// </summary>
        /// <value>The NHibernate session factory.</value>
        public ISessionFactory SessionFactory { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>Cleans the session up.</summary>
        public void CleanUp()
        {
            this.logger.Debug("Cleaning up the session");
            ISession session = ManagedWebSessionContext.Unbind(HttpContext.Current, this.SessionFactory);

            if (session != null)
            {
                if (session.Transaction != null && session.Transaction.IsActive)
                {
                    this.logger.Debug("Session has active transaction which will be rolled back");
                    session.Transaction.Rollback();
                }
                else if (HttpContext.Current.Error == null && session.IsDirty())
                {
                    this.logger.Debug("Session is dirty and error free, it will be flushed");
                    session.Flush();
                }

                this.logger.Debug("Closing session");
                session.Close();
            }

            this.logger.Debug("Finished cleaning up session");
        }

        #endregion

        #region Implemented Interfaces

        #region IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (HttpContext.Current != null)
            {
                this.CleanUp();
            }
        }

        #endregion

        #endregion
    }
}
