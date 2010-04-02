using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

using MediaCommMVC.WebTests.Framework;
using MediaCommMVC.WebTests.Pages.Movies;

using WatiN.Core;
using WatiN.Core.Constraints;

namespace MediaCommMVC.WebTests.Tests.Movies
{
    public class MoviesIndexPageTest : BrowserTestFixture
    {
        [FixtureSetUp]
        public void EnsureLogin()
        {
            WatiN.Core.Browser browser = new IE();
            this.LoginWithBrowser(browser);

            browser = new FireFox();
            this.LoginWithBrowser(browser);
        }

        private void LoginWithBrowser(Browser browser)
        {
            MoviesIndexPage.GoTo(browser);
            var page = browser.Page<MoviesIndexPage>();

            if (page.Document.Div("login").Exists)
            {
                page.LoginUsingDefaultUser();
            }
        }

        [Test]
        //[Browser(BrowserType.FireFox)]
        public void AddMoviePopIsNotDisplayed()
        {
            MoviesIndexPage.GoTo(Browser);
            var page = Browser.Page<MoviesIndexPage>();

            Assert.IsFalse(page.editMoviePopup.Exists);
        }

        [Test]
        [Browser(BrowserType.FireFox)]
        public void AddMoviePopIsDisplayedAfterDisplayClick()
        {
            MoviesIndexPage.GoTo(Browser);
            var page = Browser.Page<MoviesIndexPage>();

            page.ShowPopUpLink.Click();

            page.editMoviePopup.WaitUntilExists(10);
        }

        [Test]
        [Browser(BrowserType.FireFox)]
        public void CanAddMovie()
        {
            MoviesIndexPage.GoTo(Browser);
            var page = Browser.Page<MoviesIndexPage>();

            page.ShowPopUpLink.Click();
            page.editMoviePopup.WaitUntilExists(10);

            page.MovieTitleTextfield.TypeText("my test movie");
            page.MovieInfoLinkTextfield.TypeText("http://movieinfo.text/myMovie");
            
            page.SubmitMovieButton.Click();

            TableRow row = page.movieTable.FindRow(new Regex(".*?my test movie.*?"), 0);

            Assert.IsNotNull(row);
            Assert.IsTrue(row.InnerHtml.Contains("http://movieinfo.text/myMovie"));
        }
    }
}
