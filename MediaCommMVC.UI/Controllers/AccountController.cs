#region Using Directives

using System.Web.Mvc;
using System.Web.Security;

using MediaCommMVC.Common.Logging;
using MediaCommMVC.Core.DataInterfaces;
using MediaCommMVC.UI.ViewModel.Account;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>The account controller.</summary>
    [HandleError]
    public class AccountController : Controller
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userRepository">The user Repository.</param>
        public AccountController(ILogger logger, IUserRepository userRepository)
        {
            this.logger = logger;
            this.userRepository = userRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>The log off.</summary>
        /// <returns>Generated Code.</returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>The log on.</summary>
        /// <returns>Generated Code.</returns>
        public ActionResult LogOn()
        {
            return this.View();
        }

        /// <summary>The log on.</summary>
        /// <param name="userLogin">The model.</param>
        /// <param name="returnUrl">The return url.</param>
        /// <returns>Generated Code.</returns>
        [HttpPost]
        public ActionResult LogOn(UserLogin userLogin, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                if (this.userRepository.ValidateUser(userLogin.UserName, userLogin.Password))
                {
                    FormsAuthentication.SetAuthCookie(userLogin.UserName, userLogin.RememberMe);

                    return !string.IsNullOrEmpty(returnUrl)
                               ? (ActionResult)this.Redirect(returnUrl)
                               : this.RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(userLogin);
        }

        #endregion
    }
}
