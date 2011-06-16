using System.Web;

using MediaCommMVC.Web.Core.Common.Exceptions;
using MediaCommMVC.Web.Core.Data.NHInfrastructure.Config;
using MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping;

using NHibernate;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public class HttpContextSessionContainer : ISessionContainer
    {
        private static readonly object sessionFactoryLock = new object();

        private static ISessionFactory sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory != null)
                {
                    return sessionFactory;
                }

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

        private static ISessionFactory CreateSessionFactory()
        {
            ConfigurationGenerator configurationGenerator = new ConfigurationGenerator(new AutoMapGenerator());
            return configurationGenerator.Generate().BuildSessionFactory();
        }
    }
}