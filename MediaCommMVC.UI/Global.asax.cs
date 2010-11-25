#region Using Directives

using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.UI.Infrastructure;

using Microsoft.Practices.Unity;

#endregion

namespace MediaCommMVC.UI
{
    /// <summary>The web application.</summary>
    public class MvcApplication : HttpApplication, IUnityContainerAccessor
    {
        #region Constants and Fields

        /// <summary>The logger.</summary>
        private readonly ILogger logger = new Log4NetLogger();

        /// <summary>The unity IoC container.</summary>
        private static IUnityContainer container;

        #endregion

        #region Properties

        /// <summary>Gets the unity container.</summary>
        /// <value>The unity container.</value>
        public IUnityContainer Container
        {
            get
            {
                return container;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>Executes custom initialization code after all event handler modules have been added.</summary>
        public override void Init()
        {
            base.Init();

            this.PostAuthenticateRequest += this.MvcApplication_PostAuthenticateRequest;
        }

        #endregion

        #region Methods

        /// <summary>Configures the application during start up.</summary>
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

        /// <summary>Handles the PostAuthenticateRequest event of the MvcApplication.
        /// Gets the MediaCommUser from the auth cookie and attaches it to the HttpContext.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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