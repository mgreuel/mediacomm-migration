namespace MediaCommMVC.Tests.AcceptanceTests.Steps
{
    #region Using Directives

    using MediaCommMVC.Tests.AcceptanceTests.Helpers;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class PageInteractionSteps
    {
        #region Public Methods

        [Given(@"I have entered ""(.*?)"" as username and ""(.*?)"" as password")]
        public void GivenIHaveEnteredAUsernameAndAPassword(string username, string password)
        {
            WebBrowser.Driver.SetValue("UserName", username);
            WebBrowser.Driver.SetValue("Password", password);
        }

        [When(@"I press ""(.*?)""")]
        public void WhenIPressTheButton(string buttonId)
        {
            WebBrowser.Driver.ClickButton(buttonId);
        }

        #endregion
    }
}