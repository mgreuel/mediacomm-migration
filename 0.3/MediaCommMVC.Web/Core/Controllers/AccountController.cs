#region Using Directives

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using MediaCommMVC.Web.Core.Common.Logging;
using MediaCommMVC.Web.Core.DataInterfaces;
using MediaCommMVC.Web.Core.Model.Users;
using MediaCommMVC.Web.Core.ViewModel.Account;

using Resources;

#endregion

namespace MediaCommMVC.Web.Core.Controllers
{
    using MediaCommMVC.Web.Core.Infrastructure;

    [HandleError]
    public class AccountController : Controller
    {
        #region Constants and Fields

        private readonly IUserRepository userRepository;

        #endregion

        #region Constructors and Destructors

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #endregion

        #region Public Methods

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult LogOn()
        {
            return this.View();
        }

        [HttpPost]
        [NHibernateActionFilter]
        public ActionResult LogOn(UserLogin userLogin, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                if (this.userRepository.ValidateUser(userLogin.UserName, userLogin.Password))
                {
                    string roles = string.Empty;

                    MediaCommUser user = this.userRepository.GetUserByName(userLogin.UserName);
                    if (user.IsAdmin)
                    {
                        roles = "Administrators";
                    }

                    DateTime expiration = DateTime.Now.AddDays(7);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        version: 1,
                        name: userLogin.UserName,
                        issueDate: DateTime.Now,
                        expiration: expiration,
                        isPersistent: userLogin.RememberMe,
                        userData: roles,
                        cookiePath: FormsAuthentication.FormsCookiePath);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    httpCookie.Expires = expiration;
                    this.Response.Cookies.Add(httpCookie);

                    user.LastVisit = DateTime.Now;
                    this.userRepository.UpdateUser(user);

                    return !string.IsNullOrEmpty(returnUrl)
                               ? (ActionResult)this.Redirect(returnUrl)
                               : this.RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, Users.LoginFailed);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(userLogin);
        }

        #endregion
    }
}
