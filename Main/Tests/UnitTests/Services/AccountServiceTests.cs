using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Tests.UnitTests.Services
{
    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Services;
    using MediaCommMVC.Core.ViewModel;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class AccountServiceTests
    {
        private IAccountService accountService;

        private Mock<IUserRepository> userRepositoryMock;

        [Test]
        public void LoginDataIsValid_UnknownUsername_ReturnsFalse()
        {
            // Arrange
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "unknown" };

            // Act
            bool isValid = this.accountService.LoginDataIsValid(logOnViewModel);

            // Assert
            Assert.AreEqual(false, isValid);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullUserRepository_ThrowsArgumentNullException()
        {
            new AccountService(null);
        }

        [SetUp]
        public void SetupEachTest()
        {
            this.userRepositoryMock = new Mock<IUserRepository>();
            this.accountService = new AccountService(this.userRepositoryMock.Object);
        }
    }
}
