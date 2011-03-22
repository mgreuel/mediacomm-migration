namespace MediaCommMVC.Tests.UnitTests
{
    #region Using Directives

    using System.Web.Mvc;

    using MediaCommMVC.UI.Controllers;
    using MediaCommMVC.UI.ViewModels;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    public class AccountControllerTests
    {
        #region Constants and Fields

        private AccountController accountController;

        #endregion

        #region Public Methods

        [Test]
        public void Logon_ReturnsViewWithLogOnViewModel()
        {
            // Arrange

            // Act
            ViewResult result = this.accountController.LogOn() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(LogOnViewModel), result.Model);
        }

        [SetUp]
        public void SetupEachTest()
        {
            this.accountController = new AccountController();
        }

        #endregion
    }
}