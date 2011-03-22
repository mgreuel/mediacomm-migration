namespace MediaCommMVC.UI.Core.Controllers
{
    #region Using Directives

    using System.Web.Mvc;

    using MediaCommMVC.UI.Core.Services;
    using MediaCommMVC.UI.Core.ViewModel;

    #endregion

    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        #region Public Methods

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public ActionResult LogOn()
        {
            return this.View(new LogOnViewModel());
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            if (!this.accountService.LoginDataIsValid(logOnViewModel))
            {
                this.ModelState.AddModelError("Password", Resources.AccountResources.UserNameAndPasswordDoNoMatch);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(logOnViewModel);
            }

            return !string.IsNullOrEmpty(returnUrl)
                       ? (ActionResult)this.Redirect(returnUrl)
                       : this.RedirectToAction("Index", "Home");
        }

        #endregion
    }
}