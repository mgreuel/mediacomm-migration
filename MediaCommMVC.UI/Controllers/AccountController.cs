#region Using Directives

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;

using MediaCommMVC.UI.AccountModels;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>
    ///   The account controller.
    /// </summary>
    [HandleError]
    public class AccountController : Controller
    {
        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "AccountController" /> class.
        /// </summary>
        public AccountController()
        {
            this.FormsService = new FormsAuthenticationService();
            this.MembershipService = new AccountMembershipService();
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets FormsService.
        /// </summary>
        /// <value>The forms service.</value>
        public IFormsAuthenticationService FormsService
        {
            get;
            private set;
        }

        /// <summary>
        ///   Gets MembershipService.
        /// </summary>
        /// <value>The membership service.</value>
        public IMembershipService MembershipService
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///   The change password.
        /// </summary>
        /// <returns>The changed passsword.</returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        /// <summary>
        ///   The change password.
        /// </summary>
        /// <param name = "model">The model.</param>
        /// <returns>Generated Code.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (this.MembershipService.ChangePassword(this.User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return this.RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        ///   The change password success.
        /// </summary>
        /// <returns>Generated Code.</returns>
        public ActionResult ChangePasswordSuccess()
        {
            return this.View();
        }

        /// <summary>
        ///   The log off.
        /// </summary>
        /// <returns>Generated Code.</returns>
        public ActionResult LogOff()
        {
            this.FormsService.SignOut();

            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///   The log on.
        /// </summary>
        /// <returns>Generated Code.</returns>
        public ActionResult LogOn()
        {
            return this.View();
        }

        /// <summary>
        ///   The log on.
        /// </summary>
        /// <param name = "model">The model.</param>
        /// <param name = "returnUrl">The return url.</param>
        /// <returns>Generated Code.</returns>
        [HttpPost]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", 
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                if (this.MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    this.FormsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }
                    else
                    {
                        return this.RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region Methods

        /// <summary>
        ///   The initialize.
        /// </summary>
        /// <param name = "requestContext">The request context.</param>
        /// <exception cref = "InvalidOperationException"></exception>
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
            else
            {
                base.Initialize(requestContext);
            }
        }

        /// <summary>
        ///   The on action executing.
        /// </summary>
        /// <param name = "filterContext">The filter context.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.ViewData["PasswordLength"] = this.MembershipService.MinPasswordLength;

            base.OnActionExecuting(filterContext);
        }

        #endregion
    }
}
