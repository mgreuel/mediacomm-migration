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

    using MediaCommMVC.Core.Data.Nh.Config;
    using MediaCommMVC.Core.Data.Nh.Mapping;
    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Infrastructure.DependencyResolution;
    using MediaCommMVC.Core.Model;

    using NHibernate;
    using NHibernate.Context;

    using StructureMap;

    #endregion

    public class MvcApplication : HttpApplication
    {
        #region Constants and Fields

        private static readonly object sessionFactoryLock = new object();

        private static ISessionFactory sessionFactory;

        #endregion

        #region Properties

        public static ISessionFactory SessionFactory
        {
            get
            {
                lock (sessionFactoryLock)
                {
                    return sessionFactory ?? (sessionFactory = CreateSessionFactory());
                }
            }
        }

        #endregion

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

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            CurrentSessionContext.Bind(SessionFactory.OpenSession());
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(SessionFactory);

            if (session != null)
            {
                session.Dispose();
            }
        }

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
        
        private static ISessionFactory CreateSessionFactory()
        {
            ConfigurationGenerator configurationGenerator = new ConfigurationGenerator(new AutoMapGenerator());
            return configurationGenerator.Generate().BuildSessionFactory();
        }

        #endregion
    }
}