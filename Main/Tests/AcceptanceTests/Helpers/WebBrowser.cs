namespace MediaCommMVC.Tests.AcceptanceTests.Helpers
{
    #region Using Directives

    using System.Configuration;
    using MvcContrib.TestHelper.WatiN;
    using TechTalk.SpecFlow;
    using WatiN.Core;

    #endregion

    public static class WebBrowser
    {
        public static IE Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                {
                    ScenarioContext.Current["browser"] = new IE();
                }

                return (IE)ScenarioContext.Current["browser"];
            }
        }

        public static WatinDriver Driver
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browserDriver"))
                {
                    ScenarioContext.Current["browserDriver"] = new WatinDriver(Current, ConfigurationManager.AppSettings["baseUrl"]);
                }

                return (WatinDriver)ScenarioContext.Current["browserDriver"];
            }
        }
    }
}
