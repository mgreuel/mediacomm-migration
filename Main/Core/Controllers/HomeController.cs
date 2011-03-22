using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Core.Controllers
{
    using System.Web.Mvc;

    using MediaCommMVC.Core.ViewModels;

    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return this.View(new HomeViewModel());
        }
    }
}
