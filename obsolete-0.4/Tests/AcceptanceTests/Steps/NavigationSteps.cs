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
            WebBrowser.Current.WaitForComplete();
        }

        [Then(@"I am redirected to ""(.*?)""")]
        public void ThenIAmRedirectedTo(string url)
        {
            StringAssert.IsMatch(url.ToLowerInvariant(), WebBrowser.Driver.Url.ToLowerInvariant());
        }

        #endregion
    }
}