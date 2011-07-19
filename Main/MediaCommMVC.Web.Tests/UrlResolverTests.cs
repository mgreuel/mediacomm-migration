using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MediaCommMVC.Web.Core.Helpers;

using NUnit.Framework;

namespace MediaCommMVC.Web.Tests
{
    [TestFixture]
    public class UrlResolverTests
    {
        [Test]
        public void LinkRecognitionRegex_ValidUrlsWithLeadingSpaceOrLineBreak_UrlsAreRecognized(
            [Values(
                " http://test.com ",
                "&nbsp;www.test.com ", 
                " http://www.test.net/321", 
                "<br />www.url.de/test.html",
                "<br /> http://Test.de  ",
                " www.testme.now",
                "<br>http://abc.cd/xyz ",
                "<br/>www.mydomain.net ",
                " http://nothing.org ",
                "<p>www.mylink.de</p>",
                "<p>www.mylink.de")] string input)
        {
            Assert.IsTrue(UrlResolver.LinkRecognitionRegex.IsMatch(input), "input was: " + input + ", Match:" + UrlResolver.LinkRecognitionRegex.Match(input).Groups[3]);
        }

        [Test]
        public void LinkRecognitionRegex_UrlsInHyperLinks_UrlsAreIgnored(
            [Values(
                "<a href=\"http://test.com\">http://test.com</a>", 
                "<a href=\"www.test.com\">www.test.com</a>",
                "<a href=\"http://www.test.com/123\">http://www.test.com/123</a>",
                "<a href=\"http://www.test.com/abc.html\">Link</a>")]
            string input)
        {
            Assert.IsFalse(UrlResolver.LinkRecognitionRegex.IsMatch(input), "input was: " + input + ", Match:"  + UrlResolver.LinkRecognitionRegex.Match(input).Groups[3]);
        }
    }
}
