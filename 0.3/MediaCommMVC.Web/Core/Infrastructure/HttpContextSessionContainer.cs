namespace MediaCommMVC.Web.Core.Infrastructure
{
    #region Using Directives

    using System.Web;

    using MediaCommMVC.Web.Core.Common.Exceptions;
    using MediaCommMVC.Web.Core.Data.NHInfrastructure.Config;
    using MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping;

    using NHibernate;

    #endregion

    public class HttpContextSessionContainer : ISessionContainer
    {
        #region Constants and Fields

        private static readonly object sessionFactoryLock = new object();

        private static ISessionFactory sessionFactory;

        #endregion

        #region Properties

        public static ISessionFactory SessionFactory
        {
            get
            {
                lock (sessionFactoryLock)
                {
                    return sessionFactory ?? (sessionFactory = CreateSessionFactory());
                }
            }
        }

        public ISession CurrentSession
        {
            get
            {
                if (InternalSession == null)
                {
                    throw new SessionNotInitializedException("The NH session has not been initialized. Make sure all actions accessing the DB have the NHibernateActionFilter attribute.");
                }

                return InternalSession;
            }

            set
            {
                InternalSession = value;
            }
        }

        private static ISession InternalSession
        {
            get { return HttpContext.Current.Items["NHibernateSession"] as ISession; }
            set { HttpContext.Current.Items["NHibernateSession"] = value; }
        }

        #endregion

        #region Methods

        private static ISessionFactory CreateSessionFactory()
        {
            ConfigurationGenerator configurationGenerator = new ConfigurationGenerator(new AutoMapGenerator());
            return configurationGenerator.Generate().BuildSessionFactory();
        }

        #endregion
    }
}