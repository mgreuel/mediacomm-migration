﻿#region Using Directives

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.UI.Infrastructure;

using Microsoft.Practices.Unity;

#endregion

namespace MediaCommMVC.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>The web application.</summary>
    public class MvcApplication : HttpApplication, IUnityContainerAccessor
    {
        #region Constants and Fields

        /// <summary>
        ///   The logger.
        /// </summary>
        private readonly ILogger logger = new EntLibLogger();

        /// <summary>
        ///   The unity IoC container.
        /// </summary>
        private static IUnityContainer container;

        #endregion

        #region Properties

        /// <summary>
        ///   Gets the unity container.
        /// </summary>
        /// <value>The unity container.</value>
        public IUnityContainer Container
        {
            get
            {
                return container;
            }
        }

        #endregion

        #region Methods

        /// <summary>Configures the application during start up.</summary>
        protected void Application_Start()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize(); 

            AreaRegistration.RegisterAllAreas();

            try
            {
                if (container == null)
                {
                    container = new UnityContainer();
                }
                
                new Bootstrapper(container, this.logger).Run();

                // RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            }
            catch (HttpUnhandledException unhandledException)
            {
                this.logger.Error(unhandledException.InnerException.ToString());
                throw unhandledException.InnerException;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.ToString());
                throw;
            }
        }

        #endregion
    }
}