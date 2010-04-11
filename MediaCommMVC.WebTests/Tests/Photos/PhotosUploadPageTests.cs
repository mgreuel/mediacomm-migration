using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

using MediaCommMVC.WebTests.Framework;

namespace MediaCommMVC.WebTests.Tests.Photos
{
    [TestFixture]
    public class PhotosUploadPageTests : LoginPageTests
    {
        [Test]
        [Browser(BrowserType.FireFox)]
        public void CanUploadPhotos()
        {
            throw new NotImplementedException();
        }
    }
}
