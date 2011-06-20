using System.Security.Principal;
using System.Web;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Users;

using StructureMap;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public static class StructureMapSetup
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(InitContainer);
            return ObjectFactory.Container;
        }

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
            container.For<MediaCommIdentity>().Use(i => (MediaCommIdentity)HttpContext.Current.User.Identity);
        }
    }
}