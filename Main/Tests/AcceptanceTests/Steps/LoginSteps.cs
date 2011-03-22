namespace MediaCommMVC.Tests.AcceptanceTests.Steps
{
    #region Using Directives

    using MediaCommMVC.Tests.AcceptanceTests.Helpers;

    using NUnit.Framework;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class LoginSteps
    {
        #region Public Methods

        [Then(@"I see an error message telling me ""(.*?)""")]
        public void ThenISeeAnErrorMessageTellingMe(string message)
        {
            StringAssert.Contains(message, WebBrowser.Driver.GetHtml("content"));
        }

        #endregion
    }
}