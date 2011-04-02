namespace MediaCommMVC.Tests.UnitTests.Controllers
{
    #region Using Directives

    using System.Web.Mvc;

    using MediaCommMVC.Core.Controllers;
    using MediaCommMVC.Core.Services;
    using MediaCommMVC.Core.ViewModel;

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

        [Test]
        public void PostLogon_UsernameAndPasswordMatch_AndRedirectUrlIsProvided_RedirectResultIsReturned()
        {
            // Arrange
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "correct" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(true);

            // Act
            RedirectResult redirectResult = this.accountController.LogOn(logOnViewModel, "/Target") as RedirectResult;

            // Assert
            Assert.IsNotNull(redirectResult);
        }

        [Test]
        public void PostLogon_UsernameAndPasswordMatch_AndRedirectUrlIsProvided_RedirectResultContainsProvidedUrl()
        {
            // Arrange
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "correct" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(true);

            // Act
            RedirectResult redirectResult = this.accountController.LogOn(logOnViewModel, "/Target") as RedirectResult;

            // Assert
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("/Target", redirectResult.Url);
        }

        [Test]
        public void PostLogon_UsernameAndPasswordMatch_AndNoRedirectUrlIsProvided_RedirectToRouteResultIsReturned()
        {
            // Arrange
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "correct" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(true);

            // Act
            RedirectToRouteResult redirectToRouteResult = this.accountController.LogOn(logOnViewModel, string.Empty) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(redirectToRouteResult);
        }

        [Test]
        public void PostLogon_UsernameAndPasswordMatch_AndNoRedirectUrlIsProvided_RedirectToRouteResultIsHasHomeAsTarget()
        {
            // Arrange
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "correct" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(true);

            // Act
            RedirectToRouteResult redirectToRouteResult = this.accountController.LogOn(logOnViewModel, string.Empty) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(redirectToRouteResult);
            Assert.IsTrue(redirectToRouteResult.RouteValues.ContainsValue("Home"));
            Assert.IsTrue(redirectToRouteResult.RouteValues.ContainsValue("Index"));
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