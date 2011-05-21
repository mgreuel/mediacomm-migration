#region Using Directives

using System.Web.Mvc;
using System.Web.Routing;

using log4net.Config;

using MediaCommMVC.Web.Core.Common.Config;
using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Data;
using MediaCommMVC.Web.Core.Data.NHInfrastructure;
using MediaCommMVC.Web.Core.Data.NHInfrastructure.Config;
using MediaCommMVC.Web.Core.Data.NHInfrastructure.Mapping;
using MediaCommMVC.Web.Core.Data.Repositories;
using MediaCommMVC.Web.Core.DataInterfaces;


using WebExtensions = Combres.WebExtensions;

#endregion

namespace MediaCommMVC.Web.Core.Infrastructure
{
    using StructureMap;

    /// <summary>Boostrapper initializing the web application.</summary>
    public class Bootstrapper
    {
        #region Constants and Fields

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Bootstrapper"/> class.</summary>
        /// <param name="container">The container.</param>
        /// <param name="logger">The logger.</param>
        //public Bootstrapper(IUnityContainer container, ILogger logger)
        //{
        //    this.container = container;
        //    this.logger = logger;
        //}

        #endregion

        #region Public Methods

        /// <summary>Runs the bootstrapper.</summary>
        public void Run()
        {
            IContainer container = StructureMapSetup.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));

            RegisterRoutes();
            ConfigureLog4Net();
        }

        #endregion

        #region Methods

        /// <summary>Configures the log4net logger using the web.config.</summary>
        private static void ConfigureLog4Net()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>Registers the routes.</summary>
        private static void RegisterRoutes()
        {
            RouteCollection routes = RouteTable.Routes;

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

        }



        #endregion
    }
}