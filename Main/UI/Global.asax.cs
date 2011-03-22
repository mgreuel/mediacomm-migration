namespace MediaCommMVC.UI
{
    #region Using Directives

    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    #endregion

    public class MvcApplication : HttpApplication
    {
        #region Public Methods

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        #endregion

        #region Methods

        protected void Application_Start()
        {
            throw new NotImplementedException("Wire up Ioc Container");
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        #endregion
    }
}