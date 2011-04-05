using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Tests.AcceptanceTests.Steps
{
    using MediaCommMVC.Tests.AcceptanceTests.Helpers;

    using MvcContrib.TestHelper.Ui;

    using NUnit.Framework;

    using TechTalk.SpecFlow;

    [Binding]
    public class ForumsSteps
    {
        [Binding]
        public class StepDefinitions
        {
            [Then(@"I see the Forum Index containing 3 forums")]
            public void ThenISeeTheForumIndexContaining3Forums()
            {
                int forumCount = WebBrowser.Driver.GetRowCount("forumsTable", new List<RowFilter<string>> { new RowFilter<string>(f => f, "forum") });
                Assert.AreEqual(3, forumCount);
            }
        }
    }
}
