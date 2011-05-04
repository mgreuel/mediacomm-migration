namespace MediaCommMVC
{
    #region Using Directives

    using System;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;

    using Combres;

    using HibernatingRhinos.Profiler.Appender.NHibernate;

    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Infrastructure.DependencyResolution;

    using StructureMap;

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
                "Paged", 
                "{controller}/{action}/{id}/{name}/{page}", 
                new { controller = "Home", action = "Index", page = 1 });

            routes.MapRoute(
                "Default", 
                "{controller}/{action}/{id}/{name}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, name = UrlParameter.Optional });

            routes.MapRoute("Forums", "{controller}/{action}");
        }

        #endregion

        #region Methods

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            RouteTable.Routes.AddCombresRoute("Combres");

            IContainer container = IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));

            // remove if not profiling
            NHibernateProfiler.Initialize();

            AutomapperSetup.Initialize();
        }

        protected void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                MediaCommIdentity identity = new MediaCommIdentity(ticket);
                string[] roles = ticket.UserData.Split(',');
                GenericPrincipal principal = new GenericPrincipal(identity, roles);
                HttpContext.Current.User = principal;
            }
        }

        #endregion
    }
}