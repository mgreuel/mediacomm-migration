namespace MediaCommMVC.UI.Controllers
{
    #region Using Directives

    using System.Web.Mvc;

    using MediaCommMVC.UI.ViewModels;

    #endregion

    public class AccountController : Controller
    {
        #region Public Methods

        public ActionResult LogOn()
        {
            return this.View(new LogOnViewModel());
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            return !string.IsNullOrEmpty(returnUrl)
                       ? (ActionResult)this.Redirect(returnUrl)
                       : this.RedirectToAction("Index", "Home");
        }

        #endregion
    }
}