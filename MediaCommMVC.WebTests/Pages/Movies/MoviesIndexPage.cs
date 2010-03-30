using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace MediaCommMVC.WebTests.Pages.Movies
{
    public class MoviesIndexPage : LoginPage
    {
        [FindBy(Id = "showAddPopup")]
        public Link ShowPopUpLink;

        [FindBy(Id = "editMoviePopup")]
        public Div editMoviePopup;

        public static void GoTo(Browser browser)
        {
            browser.GoTo(ConfigurationManager.AppSettings["baseUrl"] + "/Movies");
        }
    }
}
