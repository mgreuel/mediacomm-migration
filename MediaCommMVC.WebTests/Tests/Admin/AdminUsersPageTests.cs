using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

using MediaCommMVC.WebTests.Framework;

namespace MediaCommMVC.WebTests.Tests.Admin
{
    [TestFixture]
    public class AdminUsersPageTests : LoginPageTests
    {
        [Test]
        [Browser(BrowserType.FireFox)]
        public void CanAddUser()
        {
            throw new NotImplementedException();
        }
    }
}
