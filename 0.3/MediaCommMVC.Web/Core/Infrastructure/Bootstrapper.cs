using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

using log4net.Config;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public class Bootstrapper
    {
        public void Run()
        {
            IContainer container = StructureMapSetup.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));

            RegisterRoutes();
            ConfigureLog4Net();
        }

        private static void ConfigureLog4Net()
        {
            XmlConfigurator.Configure();
        }

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

            routes.MapRoute(
                "Default", 
                "{controller}/{action}", 
                new { controller = "Home", action = "Index" });

            routes.MapRoute("Error", "Error", new { controller = "Home", action = "Error" });
        }
    }
}