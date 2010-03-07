#region Using Directives

using System.Web.Mvc;
using System.Web.Routing;

using MediaCommMVC.Common.Config;
using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Data.NHInfrastructure;
using MediaCommMVC.Data.NHInfrastructure.Config;
using MediaCommMVC.Data.NHInfrastructure.Mapping;
using MediaCommMVC.Data.Repositories;
using MediaCommMVC.UI.Controllers;

using Microsoft.Practices.Unity;

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
        private ILogger logger;

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
            this.logger.Debug("Running bootstrapper");

            this.ConfigureContainer();
            this.RegisterRoutes();
            this.RegisterControllerFactory();

            this.logger.Debug("Finished running bootstrapper");
        }

        #endregion

        #region Methods

        /// <summary>Configures the unity container.</summary>
        private void ConfigureContainer()
        {
            this.logger.Debug("Configuring unity container");

            this.container.RegisterInstance(typeof(ILogger), this.logger);
            //this.container.RegisterType(typeof(AccountController), typeof(AccountController));
            this.container.RegisterType(typeof(IConfigAccessor), typeof(FileConfigAccessor));

            this.RegisterNHibernateComponents();
            this.RegisterRepositories();

            this.logger.Debug("Finished configuring unity container");
        }

        /// <summary>Registers the controller factory.</summary>
        private void RegisterControllerFactory()
        {
            this.logger.Debug("Registering unity controller factory");

            ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
        }

        /// <summary>Registers the NNibernate components.</summary>
        private void RegisterNHibernateComponents()
        {
            this.logger.Debug("Registering nHibernate components");
            this.container.RegisterType(typeof(IAutoMapGenerator), typeof(AutoMapGenerator));
            this.container.RegisterType(typeof(IConfigurationGenerator), typeof(ConfigurationGenerator));

            WebSessionManager webSessionManager = new WebSessionManager(
                (IConfigurationGenerator)this.container.Resolve(typeof(IConfigurationGenerator)),
                (ILogger)this.container.Resolve(typeof(ILogger)));
            this.container.RegisterInstance(typeof(ISessionManager), webSessionManager);

            this.logger.Debug("Finished registering nHibernate components");
        }

        /// <summary>Registers the repositories.</summary>
        private void RegisterRepositories()
        {
            this.logger.Debug("Registering repositories");

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

            this.logger.Debug("Finished registering repositories");
        }

        /// <summary>Registers the routes.</summary>
        private void RegisterRoutes()
        {
            this.logger.Debug("Registering routes");

            RouteCollection routes = RouteTable.Routes;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
#warning clean routes

            routes.MapRoute(
                "ViewForum",
                "Forums/Forum/{id}/{page}",
                new { controller = "Forums", action = "Forum", page = 1 });

            routes.MapRoute(
                "ViewTopic",
                "Forums/Topic/{id}/{page}",
                new { controller = "Forums", action = "Topic", page = 1 });

            routes.MapRoute(
                "CreateTopic",
                "Forums/CreateTopic/{id}",
                new { controller = "Forums", action = "CreateTopic" });

            routes.MapRoute(
                "PhotoOverview",
                "Photos/Photo/{id}",
                new { controller = "Photos", action = "Photo" });

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
                "DefaultWithId",
                "{controller}/{action}/{id}");

            routes.MapRoute("Error", "Error", new { controller = "Home", action = "Error" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Home", action = "Index" });

            this.logger.Debug("Finished registering routes");
        }

        #endregion
    }
}