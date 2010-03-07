#region Using Directives

using System.Web.Mvc;

#endregion

namespace MediaCommMVC.UI.Controllers
{
    /// <summary>Controller for the welcome page.</summary>
    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
        #region Public Methods

        /// <summary>Handles Errors.</summary>
        /// <returns>The error view.</returns>
        public ActionResult Error()
        {
            return this.View();
        }

        /// <summary>The welcome page.</summary>
        /// <returns>The welcome view.</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        #endregion
    }
}