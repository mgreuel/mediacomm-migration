using System.Configuration;
using System.Security.Principal;
using System.Web;

using MarkdownSharp;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data;

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
            container.For<IImageGenerator>().Use<ImageGenerator>();
            container.For<IIdentity>().Use(i => HttpContext.Current.User.Identity);
            container.For<INotificationSender>().Use<AsyncNotificationSender>();
            container.For<Markdown>().Use(
                new Markdown(new MarkdownOptions { AutoHyperlink = true, AutoNewLines = true, EncodeProblemUrlCharacters = true }));

            MailConfiguration mailConfiguration = new MailConfiguration
            {
                MailFrom = ConfigurationManager.AppSettings["mail-from"],
                SmtpHost = ConfigurationManager.AppSettings["mail-smtpHost"],
                Username = ConfigurationManager.AppSettings["mail-username"],
                Password = ConfigurationManager.AppSettings["mail-password"]
            };
            container.For<MailConfiguration>().Use(m => mailConfiguration);
        }
    }
}