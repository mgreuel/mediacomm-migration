namespace MediaCommMVC.Tests.UnitTests.Controllers
{
    #region Using Directives

    using System.Web;
    using System.Web.Mvc;

    using MediaCommMVC.Core.Controllers;
    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Model;
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

        private Mock<IUserRepository> userRepositoryMock;

        private Mock<HttpResponseBase> httpResponseMock;

        private Mock<HttpContextBase> httpContextMock;

        private Mock<ControllerContext> controllerContextMock;

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
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "secret" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(true);
            this.userRepositoryMock.Setup(r => r.GetByName(It.IsAny<string>())).Returns(new MediaCommUser());

            // Act
            RedirectResult redirectResult = this.accountController.LogOn(logOnViewModel, "/Target") as RedirectResult;

            // Assert
            Assert.IsNotNull(redirectResult);
        }

        [Test]
        public void PostLogon_UsernameAndPasswordMatch_AndRedirectUrlIsProvided_RedirectResultContainsProvidedUrl()
        {
            // Arrange
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "secret" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(true);
            this.userRepositoryMock.Setup(r => r.GetByName(It.IsAny<string>())).Returns(new MediaCommUser());

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
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "secret" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(true);
            this.userRepositoryMock.Setup(r => r.GetByName(It.IsAny<string>())).Returns(new MediaCommUser());


            // Act
            RedirectToRouteResult redirectToRouteResult = this.accountController.LogOn(logOnViewModel, string.Empty) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(redirectToRouteResult);
        }

        [Test]
        public void PostLogon_UsernameAndPasswordMatch_AndNoRedirectUrlIsProvided_RedirectToRouteResultIsHasHomeAsTarget()
        {
            // Arrange
            LogOnViewModel logOnViewModel = new LogOnViewModel { UserName = "testUser", Password = "secret" };
            this.accountServiceMock.Setup(a => a.LoginDataIsValid(It.IsAny<LogOnViewModel>())).Returns(true);
            this.userRepositoryMock.Setup(r => r.GetByName(It.IsAny<string>())).Returns(new MediaCommUser());

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
            this.userRepositoryMock = new Mock<IUserRepository>();
            this.httpResponseMock = new Mock<HttpResponseBase>();
            this.httpContextMock = new Mock<HttpContextBase>();
            this.httpContextMock.Setup(c => c.Response).Returns(this.httpResponseMock.Object);
            this.controllerContextMock = new Mock<ControllerContext>();
            this.controllerContextMock.Setup(c => c.HttpContext).Returns(this.httpContextMock.Object);
            this.httpResponseMock.Setup(r => r.Cookies).Returns(new HttpCookieCollection());

            this.accountController = new AccountController(this.accountServiceMock.Object, this.userRepositoryMock.Object);
            this.accountController.ControllerContext = this.controllerContextMock.Object;
        }

        #endregion
    }
}