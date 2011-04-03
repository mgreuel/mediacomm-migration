namespace MediaCommMVC.Tests.AcceptanceTests.Steps
{
    #region Using Directives

    using MediaCommMVC.Tests.AcceptanceTests.Helpers;

    using NUnit.Framework;

    using TechTalk.SpecFlow;

    using WatiN.Core;

    #endregion

    [Binding]
    public class LoginSteps
    {
        #region Public Methods

        [Then(@"I see an error message telling me ""(.*?)""")]
        public void ThenISeeAnErrorMessageTellingMe(string message)
        {
            StringAssert.Contains(message, WebBrowser.Driver.GetText("content"));
        }

        [Binding]
        public class StepDefinitions
        {
            [Given(@"I am logged in")]
            public void GivenIAmLoggedIn()
            {
                WebBrowser.Driver.Navigate("/Account/Logon");
                if (WebBrowser.Current.Element(Find.ById("logindisplay")).Text.Contains("Log Off"))
                {
                    return;
                }

                PageInteractionSteps pageInteractionSteps = new PageInteractionSteps();
                pageInteractionSteps.GivenIHaveEnteredAUsernameAndAPassword("testuser", "secret");
                pageInteractionSteps.WhenIPressTheButton("loginButton");
            }
        }

        #endregion
    }
}