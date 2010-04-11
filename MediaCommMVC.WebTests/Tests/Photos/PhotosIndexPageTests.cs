using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

using MediaCommMVC.WebTests.Framework;


using WatiN.Core;

namespace MediaCommMVC.WebTests.Tests.Photos
{
    [TestFixture]
    public class PhotosIndexPageTests : LoginPageTests
    {
        [Test]
        [Browser(BrowserType.FireFox)]
        public void CanSeeNewestPhotos()
        {
            throw new NotImplementedException();
        }
    }
}
