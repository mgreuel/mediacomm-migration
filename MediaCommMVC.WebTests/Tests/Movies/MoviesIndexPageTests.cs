using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

using MediaCommMVC.WebTests.Framework;
using MediaCommMVC.WebTests.Pages.Movies;

using WatiN.Core;
using WatiN.Core.Constraints;
using WatiN.Core.DialogHandlers;

namespace MediaCommMVC.WebTests.Tests.Movies
{
    public class MoviesIndexPageTests : LoginPageTests
    {
        [Test]
        [Browser(BrowserType.FireFox)]
        public void CanAddMovie()
        {
            MoviesIndexPage.GoTo(Browser);
            var page = Browser.Page<MoviesIndexPage>();

            Assert.IsFalse(page.editMoviePopup.Exists);

            page.ShowPopUpLink.Click();
            page.editMoviePopup.WaitUntilExists(10);

            string myTestMovie = "my test movie" + Guid.NewGuid();
            page.MovieTitleTextfield.TypeText(myTestMovie);
            page.MovieInfoLinkTextfield.TypeText("http://movieinfo.text/" + myTestMovie);

            page.SubmitMovieButton.Click();

            TableRow row = page.movieTable.FindRow(new Regex(".*?" + myTestMovie + ".*?"), 0);

            Assert.IsNotNull(row);
            Assert.IsTrue(row.Text.Contains(myTestMovie));
            Assert.IsTrue(row.InnerHtml.Contains("http://movieinfo.text/" + HttpUtility.UrlPathEncode(myTestMovie)));
        }

        [Test]
        [Browser(BrowserType.IE)]
        public void CanDeleteMovie()
        {
            this.LoginWithBrowserAsDefaultAdmin(Browser);

            MoviesIndexPage.GoTo(Browser);
            var page = Browser.Page<MoviesIndexPage>();

            if (page.movieTable.TableRows.Count <= 1)
            {
                Assert.Fail("No Movie to be deleted");
            }

            TableRow row =  page.movieTable.TableRows[1];
            Link delLink = row.Link(row.Id.Replace("movie_", "del_"));

            ConfirmDialogHandler handler = new ConfirmDialogHandler();
            using (new UseDialogOnce(Browser.DialogWatcher, handler))
            {
                
                delLink.ClickNoWait();
                handler.WaitUntilExists(5);
                handler.OKButton.Click();
            }

            Browser.WaitForComplete();

            Assert.IsFalse(row.Exists);
        }
    }
}
