#region Using Directives

using System;
using System.Web;
using System.Web.Mvc;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.Core.Model.Users;
using MediaCommMVC.UI.Infrastructure;

using Microsoft.Practices.Unity;

#endregion

namespace MediaCommMVC.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    ///   The web application.
    /// </summary>
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

        /// <summary>Handles the AuthenticateRequest event of the Application control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (this.User != null)
            {
                IUserRepository userRepository = this.Container.Resolve<IUserRepository>();
                MediaCommUser user = userRepository.GetUserByName(this.User.Identity.Name);

                WebContext.CurrentUser = user;
            }
            else
            {
                WebContext.CurrentUser = new MediaCommUser(string.Empty, string.Empty, string.Empty);
            }
        }

        /// <summary>
        ///   Configures the application during start up.
        /// </summary>
        protected void Application_Start()
        {
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