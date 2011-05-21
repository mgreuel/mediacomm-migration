#region Using Directives

using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Infrastructure;


#endregion

namespace MediaCommMVC.Web
{
    public class MvcApplication : HttpApplication
    {
        #region Constants and Fields

        private readonly ILogger logger = new Log4NetLogger();

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public override void Init()
        {
            base.Init();

            this.PostAuthenticateRequest += this.MvcApplication_PostAuthenticateRequest;
        }

        #endregion

        #region Methods

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            try
            {
                //new Bootstrapper(container, this.logger).Run();
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