namespace MediaCommMVC
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AutoMapper;

    using Combres;

    using HibernatingRhinos.Profiler.Appender.NHibernate;

    using MediaCommMVC.Core.Data.Nh.Config;
    using MediaCommMVC.Core.Data.Nh.Mapping;
    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Infrastructure.DependencyResolution;
    using MediaCommMVC.Core.Model;
    using MediaCommMVC.Core.ViewModel;

    using NHibernate;
    using NHibernate.Context;

    using StructureMap;

    #endregion

    public class MvcApplication : HttpApplication
    {
        #region Constants and Fields

        private static ISessionFactory sessionFactory;

        private static object sessionFactoryLock = new object();

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

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        #endregion

        #region Methods

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            CurrentSessionContext.Bind(SessionFactory.OpenSession());
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            CurrentSessionContext.Unbind(SessionFactory).Dispose();
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

        private static ISessionFactory CreateSessionFactory()
        {
            ConfigurationGenerator configurationGenerator = new ConfigurationGenerator(new AutoMapGenerator());
            return configurationGenerator.Generate().BuildSessionFactory();
        }

        #endregion
    }
}