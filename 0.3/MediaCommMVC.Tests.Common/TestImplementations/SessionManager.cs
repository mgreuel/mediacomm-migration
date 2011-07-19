#region Using Directives

using FluentNHibernate.Cfg;

using MediaCommMVC.Data.NHInfrastructure;
using MediaCommMVC.Data.NHInfrastructure.Config;

using NHibernate;

#endregion

namespace MediaCommMVC.Tests.TestImplementations
{
    /// <summary>Manages the NHibernate sessions.</summary>
    public class SessionManager : ISessionManager
    {
        #region Constants and Fields

        /// <summary>The NHibernate session factory.</summary>
        private readonly ISessionFactory sessionFactory;

        /// <summary>The session.</summary>
        private ISession session;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManager"/> class.
        /// </summary>
        /// <param name="configurationGenerator">The configuration generator.</param>
        public SessionManager(IConfigurationGenerator configurationGenerator)
        {
            // The default configuration generator
            this.sessionFactory = BuildSessionFactory(configurationGenerator);
        }

        #endregion

        #region Properties

        /// <summary>Gets the NHibernate session.</summary>
        /// <value>The NHibernate session.</value>
        public ISession Session
        {
            get
            {
                return this.session;
            }
        }

        /// <summary>Gets the NHibernate session factory.</summary>
        /// <value>The NHibernate session factory.</value>
        public ISessionFactory SessionFactory
        {
            get
            {
                return this.sessionFactory;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>Creates a new session.</summary>
        public void CreateNewSession()
        {
            if (this.session != null && this.session.IsOpen)
            {
                this.session.Close();
            }

            this.session = this.sessionFactory.OpenSession();
        }

        #endregion

        #region Methods

        /// <summary>Builds the NHibernate session factory.</summary>
        /// <param name="configurationGenerator">The configuration generator.</param>
        /// <returns>The session factory.</returns>
        private static ISessionFactory BuildSessionFactory(IConfigurationGenerator configurationGenerator)
        {
            FluentConfiguration configuration = configurationGenerator.Generate();

            return configuration.BuildSessionFactory();
        }

        #endregion
    }
}