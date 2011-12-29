using System.Web.Mvc;
using System.Web.Routing;

using log4net.Config;

using StructureMap;

namespace MediaCommMVC.Web.Core.Infrastructure
{
    public sealed class Bootstrapper
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

            RegisterIgnores(routes);

            RegisterForumsRoutes(routes);
            RegisterAccountRoutes(routes);
            RegisterPhotosRoutes(routes);
            RegisterUsersRoutes(routes);
            RegisterGeneralRoutes(routes);
            RegisterMoviesRoutes(routes);
            RegisterVideoRoutes(routes);
            RegisterAdminRoutes(routes);
        }

        private static void RegisterAdminRoutes(RouteCollection routes)
        {
            routes.MapRoute("CreateUser", "Admin/CreateUser", new { controller = "Admin", action = "CreateUser" });
            routes.MapRoute("UserCreated", "Admin/UserCreated", new { controller = "Admin", action = "UserCreated" });
            routes.MapRoute("CategoryCreated", "Admin/CategoryCreated", new { controller = "Admin", action = "CategoryCreated" });
            routes.MapRoute("CreateForum", "Admin/CreateForum", new { controller = "Admin", action = "CreateForum" });
            routes.MapRoute("CreatePhotoCategory", "Admin/CreatePhotoCategory", new { controller = "Admin", action = "CreatePhotoCategory" });
            routes.MapRoute("CreateVideoCategory", "Admin/CreateVideoCategory", new { controller = "Admin", action = "CreateVideoCategory" });
        }

        private static void RegisterIgnores(RouteCollection routes)
        {
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }

        private static void RegisterGeneralRoutes(RouteCollection routes)
        {
            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" });
            routes.MapRoute("Error", "Error", new { controller = "Home", action = "Error" });
        }

        private static void RegisterUsersRoutes(RouteCollection routes)
        {
            routes.MapRoute("MyProfile", "Users/MyProfile", new { controller = "Users", action = "MyProfile" });
            routes.MapRoute("UsersIndex", "Users", new { controller = "Users", action = "Index" });
            routes.MapRoute("Profile", "Users/{username}", new { controller = "Users", action = "Profile" });
        }

        private static void RegisterMoviesRoutes(RouteCollection routes)
        {
            routes.MapRoute("MoviesIndex", "Movies", new { controller = "Movies", action = "Index" });
            routes.MapRoute("DeleteMovie", "Movies/DeleteMovie/{id}", new { controller = "Movies", action = "DeleteMovie" });
        }

        private static void RegisterVideoRoutes(RouteCollection routes)
        {
            routes.MapRoute("AddVideo", "Videos/AddVideo", new { controller = "Videos", action = "AddVideo" });
            routes.MapRoute("VideoUploadSuccessFull", "Videos/UploadSuccessFull", new { controller = "Videos", action = "UploadSuccessFull" });
            routes.MapRoute("VideosCategory", "Videos/Category/{id}/{name}", new { controller = "Videos", action = "Category" });
            routes.MapRoute("Video", "Videos/Video/{id}/{name}", new { controller = "Videos", action = "Video" });
            routes.MapRoute("VideoCategories", "Videos/GetCategories", new { controller = "Videos", action = "GetCategories" });
            routes.MapRoute("VideoThumbnail", "Videos/Thumbnail/{id}/{name}", new { controller = "Videos", action = "Thumbnail" });
        }

        private static void RegisterAccountRoutes(RouteCollection routes)
        {
            routes.MapRoute("LogOn", "Account/LogOn", new { controller = "Account", action = "LogOn" });
            routes.MapRoute("LogOff", "Account/LogOff", new { controller = "Account", action = "LogOff" });
        }

        private static void RegisterPhotosRoutes(RouteCollection routes)
        {
            routes.MapRoute("PhotosCategory", "Photos/Category/{id}/{name}", new { controller = "Photos", action = "Category" });
            routes.MapRoute("GetPhoto", "Photos/Photo/{id}/{size}", new { controller = "Photos", action = "Photo" });
            routes.MapRoute("CompletePhotoUpload", "Photos/CompleteUpload", new { controller = "Photos", action = "CompleteUpload" });
            routes.MapRoute("PhotoUploadSuccessFull", "Photos/UploadSuccessFull", new { controller = "Photos", action = "UploadSuccessFull" });
            routes.MapRoute("PhotoCategories", "Photos/GetCategories", new { controller = "Photos", action = "GetCategories" });
            routes.MapRoute("PhotoAlbumSuggest", "Photos/GetAlbumsForCategoryId/{id}", new { controller = "Photos", action = "GetAlbumsForCategoryId" });
            routes.MapRoute("PhotoFileUpload", "Photos/UploadFile", new { controller = "Photos", action = "UploadFile" });
            routes.MapRoute("PhotoUpload", "Photos/Upload", new { controller = "Photos", action = "Upload" });
            routes.MapRoute("PhotoAlbum", "Photos/Album/{id}/{name}", new { controller = "Photos", action = "Album" });
        }

        private static void RegisterForumsRoutes(RouteCollection routes)
        {
            routes.MapRoute("ViewForum", "Forums/Forum/{id}/{name}/{page}", new { controller = "Forums", action = "Forum", page = 1 });
            routes.MapRoute("ViewTopic", "Forums/Topic/{id}/{name}/{page}", new { controller = "Forums", action = "Topic", page = 1 });
            routes.MapRoute("DeletePost", "Forums/DeletePost/{id}", new { controller = "Forums", action = "DeletePost" });
            routes.MapRoute("EditPost", "Forums/EditPost/{id}", new { controller = "Forums", action = "EditPost" });
            routes.MapRoute("FirstNewPostInTopic", "Forums/FirstNewPostInTopic/{id}", new { controller = "Forums", action = "FirstNewPostInTopic" });
            routes.MapRoute("AnswerPoll", "Forums/AnswerPoll", new { controller = "Forums", action = "AnswerPoll" });
            routes.MapRoute("ForumsIndex", "Forums", new { controller = "Forums", action = "Index" });
            routes.MapRoute("CreateTopic", "Forums/CreateTopic/{id}", new { controller = "Forums", action = "CreateTopic" });
        }
    }
}