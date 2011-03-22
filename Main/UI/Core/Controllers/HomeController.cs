namespace MediaCommMVC.UI.Core.Controllers
{
    #region Using Directives

    using System.Web.Mvc;

    using MediaCommMVC.UI.Core.ViewModel;

    #endregion

    public class HomeController : Controller
    {
        #region Public Methods

        public ViewResult Index()
        {
            return this.View(new HomeViewModel());
        }

        #endregion
    }
}