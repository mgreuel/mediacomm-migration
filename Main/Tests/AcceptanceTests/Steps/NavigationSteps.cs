namespace MediaCommMVC.Tests.AcceptanceTests.Steps
{
    #region Using Directives

    using MediaCommMVC.Tests.AcceptanceTests.Helpers;

    using NUnit.Framework;

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class NavigationSteps
    {
        #region Public Methods

        [Given(@"I navigate to ""(.*?)""")]
        public void GivenINavigateTo(string url)
        {
            WebBrowser.Driver.Navigate(url);
        }

        [Then(@"I am redirected to ""(.*?)""")]
        public void ThenIAmRedirectedTo(string url)
        {
            Assert.AreEqual(url, WebBrowser.Driver.Url);
        }

        #endregion
    }
}