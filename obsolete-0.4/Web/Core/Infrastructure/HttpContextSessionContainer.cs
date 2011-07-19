namespace MediaCommMVC.Core.Infrastructure
{
    #region Using Directives

    using System.Web;

    using MediaCommMVC.Core.Data.Nh.Config;
    using MediaCommMVC.Core.Data.Nh.Mapping;

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
                    InternalSession = SessionFactory.OpenSession();
                    InternalSession.BeginTransaction();
                }

                return InternalSession;
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