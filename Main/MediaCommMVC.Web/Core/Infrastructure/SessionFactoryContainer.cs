using MediaCommMVC.Web.Core.Data.NHInfrastructure.Config;
using MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping;

using NHibernate;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public static class SessionFactoryContainer 
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

        private static ISessionFactory CreateSessionFactory()
        {
            ConfigurationGenerator configurationGenerator = new ConfigurationGenerator(new AutoMapGenerator());
            return configurationGenerator.Generate().BuildSessionFactory();
        }
    }
}