namespace MediaCommMVC.Web.Core.Infrastructure
{
    #region Using Directives

    using MediaCommMVC.Web.Core.Common.Config;
    using MediaCommMVC.Web.Core.Common.Logging;
    using MediaCommMVC.Web.Core.Data;

    using StructureMap;

    #endregion

    public static class StructureMapSetup
    {
        #region Public Methods

        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(InitContainer);
            return ObjectFactory.Container;
        }

        #endregion

        #region Methods

        private static void InitContainer(IInitializationExpression container)
        {
            container.Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });

            container.For<ISessionContainer>().Use<HttpContextSessionContainer>();
            container.For<IConfigAccessor>().Use<FileConfigAccessor>();
            container.For<ILogger>().Use<Log4NetLogger>();
            container.For<IImageGenerator>().Use<MixedImageGenerator>();

            //container.For<MediaCommUser>().HybridHttpOrThreadLocalScoped().Use(u => u.GetInstance<IUserRepository>().GetByName(HttpContext.Current.User.Identity.Name));
        }

        #endregion
    }
}