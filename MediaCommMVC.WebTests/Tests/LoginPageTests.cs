using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MbUnit.Framework;

using MediaCommMVC.WebTests.Framework;
using MediaCommMVC.WebTests.Pages;
using MediaCommMVC.WebTests.Pages.Movies;

using WatiN.Core;

namespace MediaCommMVC.WebTests.Tests
{
    public class LoginPageTests : BrowserTestFixture
    {
        protected void LoginWithBrowser(Browser browser)
        {
            LoginPage.GoTo(browser);
            var page = browser.Page<LoginPage>();

            if (page.Document.Div("login").Exists)
            {
                page.LoginUsingDefaultUser();
            }
        }

        protected void LoginWithBrowserAsDefaultAdmin(Browser browser)
        {
            LoginPage.GoTo(browser);
            var page = browser.Page<LoginPage>();

            if (page.Document.Div("login").Exists)
            {
                page.LoginUsingDefaultAdmin();
            }
        }

        [FixtureSetUp]
        public void EnsureLogin()
        {
            Browser ie = new IE();
            this.LoginWithBrowser(ie);

            Browser ff = new FireFox();
            this.LoginWithBrowser(ff);
        }
    }
}
