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

        [FindBy(Id = "ui-dialog-title-editMovie")]
        public Span editMoviePopup;

        [FindBy(Name = "Movie.Title")]
        public TextField MovieTitleTextfield;

        [FindBy(Name = "Movie.InfoLink")]
        public TextField MovieInfoLinkTextfield;

        [FindBy(Id = "languageID")]
        public SelectList MovieLanguageSelect;

        [FindBy(Id = "qualityID")]
        public SelectList MovieQualitySelect;

        [FindBy(Id = "submitMovie")]
        public Button SubmitMovieButton;

        [FindBy(Id = "movieTable")]
        public Table movieTable;

        public static void GoTo(Browser browser)
        {
            browser.GoTo(ConfigurationManager.AppSettings["baseUrl"] + "/Movies");
        }
    }
}
