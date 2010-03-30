using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

using MediaCommMVC.WebTests.Framework;
using MediaCommMVC.WebTests.Pages.Movies;

using WatiN.Core;

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
        [Browser(BrowserType.IE)]
        [Browser(BrowserType.FireFox)]
        public void CanGoToPage()
        {
            MoviesIndexPage.GoTo(Browser);
            var page = Browser.Page<MoviesIndexPage>();

            Assert.IsTrue(page.Document.Table("movieTable").Exists);
        }

        [Test]
        [Browser(BrowserType.IE)]
        [Browser(BrowserType.FireFox)]
        public void AddMoviePopIsNotDisplayed()
        {
            MoviesIndexPage.GoTo(Browser);
            var page = Browser.Page<MoviesIndexPage>();

            Div popupDiv = page.Document.Div("editMoviePopup");
            Assert.IsTrue(popupDiv.GetAttributeValue("style").Contains("display: none;"));
        }

        [Test]
        [Browser(BrowserType.IE)]
        [Browser(BrowserType.FireFox)]
        public void AddMoviePopIsDisplayedAfterDisplayClick()
        {
            MoviesIndexPage.GoTo(Browser);
            var page = Browser.Page<MoviesIndexPage>();

            page.ShowPopUpLink.Click();

            Div popupDiv = page.Document.Div("editMoviePopup");

            Console.WriteLine();
        }
    }
}
