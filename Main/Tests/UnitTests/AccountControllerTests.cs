namespace MediaCommMVC.Tests.UnitTests
{
    #region Using Directives

    using System.Web.Mvc;

    using MediaCommMVC.UI.Core.Controllers;
    using MediaCommMVC.UI.Core.Services;
    using MediaCommMVC.UI.Core.ViewModel;

    using MvcContrib.TestHelper;

    using Moq;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    public class AccountControllerTests
    {
        #region Constants and Fields

        private AccountController accountController;

        private Mock<IAccountService> accountServiceMock;

        #endregion

        #region Public Methods

        [Test]
        public void GetLogon_ReturnsViewWithLogOnViewModel()
        {
            // Arrange

            // Act
            ViewResult result = this.accountController.LogOn() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(LogOnViewModel), result.Model);
        }

        [Test]
        public void PostLogon_UsernameAndPasswordDoNotMatch_LogOnViewIsReturned()
        {
            // Arrange
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "wrong" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(false);

            // Act
            ViewResult result = this.accountController.LogOn(logOnViewModel, string.Empty) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(LogOnViewModel), result.Model);
        }

        [Test]
        public void PostLogon_UsernameAndPasswordDoNotMatch_ModelStateIsInvalid()
        {
            // Arrange
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "wrong" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(false);

            // Act
            this.accountController.LogOn(logOnViewModel, string.Empty);

            // Assert
            Assert.IsFalse(this.accountController.ModelState.IsValid);
        }

        [SetUp]
        public void SetupEachTest()
        {
            this.accountServiceMock = new Mock<IAccountService>();
            this.accountController = new AccountController(this.accountServiceMock.Object);
        }

        #endregion
    }
}