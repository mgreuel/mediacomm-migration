namespace MediaCommMVC.Core.Controllers
{
    #region Using Directives

    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Model;
    using MediaCommMVC.Core.Services;
    using MediaCommMVC.Core.ViewModel;

    using Resources;

    #endregion

    [NHibernateActionFilter]
    public class AccountController : Controller
    {
        #region Constants and Fields

        private readonly IAccountService accountService;

        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors and Destructors

        public AccountController(IAccountService accountService, IUserRepository userRepository)
        {
            this.accountService = accountService;
            this.userRepository = userRepository;
        }

        #endregion

        #region Public Methods

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return this.RedirectToAction("Logon");
        }

        public ActionResult LogOn()
        {
            return this.View(new LogOnViewModel());
        }

        [HttpPost]
        [NHibernateActionFilter]
        public ActionResult LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            if (!this.accountService.LoginDataIsValid(logOnViewModel))
            {
                this.ModelState.AddModelError("Password", AccountResources.UserNameAndPasswordDoNoMatch);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(logOnViewModel);
            }

            string roles = string.Empty;

            MediaCommUser user = this.userRepository.GetByName(logOnViewModel.UserName);
            if (user.IsAdmin)
            {
                roles = "Administrator";
            }

            DateTime expiration = DateTime.Now.AddYears(1);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                version: 1, 
                name: logOnViewModel.UserName, 
                issueDate: DateTime.Now, 
                expiration: expiration, 
                isPersistent: logOnViewModel.RememberMe, 
                userData: roles, 
                cookiePath: FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { Expires = expiration };
            this.Response.Cookies.Add(httpCookie);

            return !string.IsNullOrEmpty(returnUrl) ? (ActionResult)this.Redirect(returnUrl) : this.RedirectToAction("Index", "Home");
        }

        #endregion
    }
}