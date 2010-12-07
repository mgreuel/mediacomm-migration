#region Using Directives

using System.Web.Mvc;
using System.Web.Routing;

using log4net.Config;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Data;
using MediaCommMVC.Data.NHInfrastructure;
using MediaCommMVC.Data.NHInfrastructure.Config;
using MediaCommMVC.Data.NHInfrastructure.Mapping;
using MediaCommMVC.Data.Repositories;

using Microsoft.Practices.Unity;

using WebExtensions = Combres.WebExtensions;

#endregion

namespace MediaCommMVC.UI.Infrastructure
{
    /// <summary>Boostrapper initializing the web application.</summary>
    public class Bootstrapper
    {
        #region Constants and Fields

        /// <summary>The unity container.</summary>
        private readonly IUnityContainer container;

        /// <summary>The logger.
        /// It is created in the Global.asax file.</summary>
        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Bootstrapper"/> class.</summary>
        /// <param name="container">The container.</param>
        /// <param name="logger">The logger.</param>
        public Bootstrapper(IUnityContainer container, ILogger logger)
        {
            this.container = container;
            this.logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>Runs the bootstrapper.</summary>
        public void Run()
        {
            this.ConfigureContainer();
            RegisterRoutes();
            RegisterControllerFactory();
            ConfigureLog4Net();
        }

        #endregion

        #region Methods

        /// <summary>Configures the log4net logger using the web.config.</summary>
        private static void ConfigureLog4Net()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>Registers the controller factory.</summary>
        private static void RegisterControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
        }

        /// <summary>Registers the routes.</summary>
        private static void RegisterRoutes()
        {
            RouteCollection routes = RouteTable.Routes;

            WebExtensions.AddCombresRoute(routes, "Combres Route");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
#warning clean routes

            routes.MapRoute(
                "ViewForum", 
                "Forums/Forum/{id}/{name}/{page}", 
                new { controller = "Forums", action = "Forum", page = 1 });

            routes.MapRoute(
                "ViewTopic", 
                "Forums/Topic/{id}/{name}/{page}", 
                new { controller = "Forums", action = "Topic", page = 1 });

            routes.MapRoute(
                "CreateTopic", 
                "Forums/CreateTopic/{id}", 
                new { controller = "Forums", action = "CreateTopic" });

            routes.MapRoute(
                "GetPhoto", 
                "Photos/Photo/{id}/{size}", 
                new { controller = "Photos", action = "Photo" });

            routes.MapRoute(
                "MyProfile", 
                "Users/MyProfile", 
                new { controller = "Users", action = "MyProfile" });

            routes.MapRoute(
                "Profile", 
                "Users/{username}", 
                new { controller = "Users", action = "Profile" });

            routes.MapRoute(
                "DefaultWithIdAndName", 
                "{controller}/{action}/{id}/{name}");

            routes.MapRoute(
                "DefaultWithId", 
                "{controller}/{action}/{id}");

            routes.MapRoute("Error", "Error", new { controller = "Home", action = "Error" });

            routes.MapRoute(
                "Default", 
                "{controller}/{action}", 
                new { controller = "Home", action = "Index" });
        }

        /// <summary>Configures the unity container.</summary>
        private void ConfigureContainer()
        {
            this.container.RegisterInstance(typeof(ILogger), this.logger);
            this.container.RegisterType(typeof(IConfigAccessor), typeof(FileConfigAccessor));
            this.container.RegisterType(typeof(IImageGenerator), typeof(MixedImageGenerator));

            this.RegisterNHibernateComponents();
            this.RegisterRepositories();
        }

        /// <summary>Registers the NNibernate components.</summary>
        private void RegisterNHibernateComponents()
        {
            this.container.RegisterType(typeof(IAutoMapGenerator), typeof(AutoMapGenerator));
            this.container.RegisterType(typeof(IConfigurationGenerator), typeof(ConfigurationGenerator));

            WebSessionManager webSessionManager =
                new WebSessionManager(
                    (IConfigurationGenerator)this.container.Resolve(typeof(IConfigurationGenerator)), 
                    (ILogger)this.container.Resolve(typeof(ILogger)));
            this.container.RegisterInstance(typeof(ISessionManager), webSessionManager);
        }

        /// <summary>Registers the repositories.</summary>
        private void RegisterRepositories()
        {
            this.container.RegisterType(
                typeof(IForumRepository), 
                typeof(ForumRepository), 
                new HttpContextLifetimeManager<ForumRepository>());
            this.container.RegisterType(
                typeof(IPhotoRepository), 
                typeof(PhotoRepository), 
                new HttpContextLifetimeManager<PhotoRepository>());
            this.container.RegisterType(
                typeof(IMovieRepository), 
                typeof(MovieRepository), 
                new HttpContextLifetimeManager<MovieRepository>());
            this.container.RegisterType(
                typeof(IUserRepository), 
                typeof(UserRepository), 
                new HttpContextLifetimeManager<UserRepository>());
        }

        #endregion
    }
}