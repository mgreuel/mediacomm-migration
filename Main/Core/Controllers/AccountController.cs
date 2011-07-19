using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Core.Controllers
{
    using System.Web.Mvc;

    using MediaCommMVC.Core.ViewModels;

    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return this.View(new LogOnViewModel());
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel logOnViewModel, string returnUrl)
        {
            return !string.IsNullOrEmpty(returnUrl) ? (ActionResult)this.Redirect(returnUrl) : this.RedirectToAction("Index", "Home");
        }
    }
}
