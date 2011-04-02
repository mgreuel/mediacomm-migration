namespace MediaCommMVC.Core.Infrastructure.DependencyResolution
{
    #region Using Directives

    using System;
    using System.Web.Mvc;

    using MediaCommMVC.Core.Data;

    using NHibernate;

    using StructureMap;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    #endregion

    public static class IoC
    {
        #region Public Methods

        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(InitContainer);
            return ObjectFactory.Container;
        }

        private static void InitContainer(IInitializationExpression container)
        {
            container.For<ISession>().Use(s => MvcApplication.SessionFactory.GetCurrentSession());
            container.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.Convention<NhRepositoryConvention>();
            });
        }


        //private static IUnityContainer CreateUnityContainer()
        //{
        //    UnityContainer container = new UnityContainer();
        //    container.RegisterType<IAccountService, AccountService>();

        //    List<Type> controllerTypes =
        //        (from t in Assembly.GetCallingAssembly().GetTypes() where typeof(IController).IsAssignableFrom(t) && !t.IsAbstract select t).ToList();

        //    controllerTypes.ForEach(t => container.RegisterType(t));

        //    container.RegisterType<IAutoMapGenerator, AutoMapGenerator>();

        //    container.RegisterType<IUserRepository, NhUserRepository>();

        //    IConfigurationGenerator configurationGenerator = container.Resolve<IConfigurationGenerator>();
        //    SessionFactory = configurationGenerator.Generate().BuildSessionFactory();
        //    container.RegisterInstance(SessionFactory);

        //    return container;
        //}

        #endregion
    }
}