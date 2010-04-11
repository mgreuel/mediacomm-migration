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
    public class PhotosAlbumPageTests : LoginPageTests
    {
        [Test]
        [Browser(BrowserType.FireFox)]
        public void CanSeePhotoAlbum()
        {
            throw new NotImplementedException();
        }
    }
}
