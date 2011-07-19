using System;
using System.Linq;
using System.Text;

namespace MediaCommMVC.Tests.UnitTests.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using MediaCommMVC.Core.Controllers;
    using MediaCommMVC.Core.Data;
    using MediaCommMVC.Core.Infrastructure;
    using MediaCommMVC.Core.Model;
    using MediaCommMVC.Core.Model.Forums;
    using MediaCommMVC.Core.Services;
    using MediaCommMVC.Core.ViewModel;
    using MediaCommMVC.Core.ViewModels.Pages.Forums;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ForumsControllerTests
    {
        private ForumsController forumsController;

        private Mock<IForumsRepository> forumsRepositoryMock;

        private Mock<IUserRepository> userRepositoryMock;

        private Mock<IForumsService> forumsServiceMock;

        [Test]
        public void Index_ReturnsViewWithForumsIndexViewModel()
        {
            // Arrange

            // Act
            ViewResult result = this.forumsController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ForumsIndexViewModel), result.Model);
        }

        [Test]
        public void Index_TwoForums_ModelContainsTwoForums()
        {
            // Arrange
            this.forumsRepositoryMock.Setup(r => r.GetAll()).Returns(new List<Forum> { new Forum { Title = "forum1" }, new Forum { Title = "forum2" } });

            // Act
            ViewResult result = this.forumsController.Index() as ViewResult;

            // Assert
            ForumsIndexViewModel forumsIndexViewModel = result.Model as ForumsIndexViewModel;
            Assert.AreEqual(2, forumsIndexViewModel.Forums.Count());
        }

        [SetUp]
        public void SetUpEachTest()
        {
            AutomapperSetup.Initialize();
            this.forumsRepositoryMock = new Mock<IForumsRepository>();
            this.userRepositoryMock = new Mock<IUserRepository>();
            this.forumsServiceMock = new Mock<IForumsService>();

            this.forumsController = new ForumsController(this.forumsRepositoryMock.Object, this.userRepositoryMock.Object, this.forumsServiceMock.Object);
        }
    }
}
