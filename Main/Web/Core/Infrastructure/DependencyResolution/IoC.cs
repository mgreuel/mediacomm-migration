namespace MediaCommMVC.Core.Infrastructure.DependencyResolution
{
    #region Using Directives

    using System.Web;

    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Model;

    using StructureMap;

    #endregion

    public static class IoC
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
            container.For<ISessionContainer>().Use<HttpContextSessionContainer>();
            container.Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.Convention<NhRepositoryConvention>();
                });

            container.For<MediaCommUser>().HybridHttpOrThreadLocalScoped().Use(u => u.GetInstance<IUserRepository>().GetByName(HttpContext.Current.User.Identity.Name));
        }

        #endregion
    }
}