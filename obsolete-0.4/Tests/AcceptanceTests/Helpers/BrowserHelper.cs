namespace MediaCommMVC.Tests.AcceptanceTests.Helpers
{
    #region Using Directives

    using TechTalk.SpecFlow;

    #endregion

    [Binding]
    public class BrowserHelper
    {
        #region Public Methods

        [AfterScenario]
        public static void CloseBrowser()
        {
            if (WebBrowser.Current != null)
            {
                WebBrowser.Current.Close();
            }
        }

        #endregion
    }
}