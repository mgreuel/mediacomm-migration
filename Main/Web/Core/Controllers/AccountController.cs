namespace MediaCommMVC.Core.Controllers
{
    #region Using Directives

    using System.Web.Mvc;

    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Services;
    using MediaCommMVC.Core.ViewModel;

    using Resources;

    #endregion

    public class AccountController : Controller
    {
        #region Constants and Fields

        private readonly IAccountService accountService;

        #endregion

        #region Constructors and Destructors

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        #endregion

        #region Public Methods

        public ActionResult LogOn()
        {
            return this.View(new LogOnViewModel());
        }

        [HttpPost]
        [TransactionFilter]
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

            return !string.IsNullOrEmpty(returnUrl) ? (ActionResult)this.Redirect(returnUrl) : this.RedirectToAction("Index", "Home");
        }

        #endregion
    }
}