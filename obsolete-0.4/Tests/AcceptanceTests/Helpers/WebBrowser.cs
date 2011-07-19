namespace MediaCommMVC.Tests.AcceptanceTests.Helpers
{
    #region Using Directives

    using System.Configuration;

    using TechTalk.SpecFlow;

    using WatiN.Core;

    #endregion

    public static class WebBrowser
    {
        #region Properties

        public static Browser Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                {
                    ScenarioContext.Current["browser"] = new IE();
                }

                return (Browser)ScenarioContext.Current["browser"];
            }
        }

        public static WatinDriver Driver
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browserDriver"))
                {
                    ScenarioContext.Current["browserDriver"] = new WatinDriver(
                        Current, ConfigurationManager.AppSettings["baseUrl"]);
                }

                return (WatinDriver)ScenarioContext.Current["browserDriver"];
            }
        }

        #endregion
    }
}