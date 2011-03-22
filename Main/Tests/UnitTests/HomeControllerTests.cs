namespace MediaCommMVC.Tests.UnitTests
{
    #region Using Directives

    using System.Web.Mvc;

    using MediaCommMVC.Core.Controllers;
    using MediaCommMVC.Core.ViewModel;

    using NUnit.Framework;

    #endregion

    [TestFixture]
    public class HomeControllerTests
    {
        #region Constants and Fields

        private HomeController homeController;

        #endregion

        #region Public Methods

        [Test]
        public void Logon_ReturnsViewWithLogOnViewModel()
        {
            // Arrange

            // Act
            ViewResult result = this.homeController.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HomeViewModel), result.Model);
        }

        [SetUp]
        public void SetupEachTest()
        {
            this.homeController = new HomeController();
        }

        #endregion
    }
}