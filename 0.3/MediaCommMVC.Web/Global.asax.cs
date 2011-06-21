using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.Infrastructure;

namespace MediaCommMVC.Web
{
    public class MvcApplication : HttpApplication
    {
        private readonly ILogger logger = new Log4NetLogger();

        public override void Init()
        {
            base.Init();

            this.PostAuthenticateRequest += this.MvcApplication_PostAuthenticateRequest;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            try
            {
                new Bootstrapper().Run();
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
    }
}