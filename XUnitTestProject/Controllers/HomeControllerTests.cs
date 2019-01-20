using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AddressManagement.Controllers;
using Xunit;
using AddressManagement.Models;

namespace TestingControllersSample.Tests.UnitTests
{
    public class HomeControllerTests
    {
        #region Indexアクション
        [Fact]
        public IActionResult Index_ReturnsAViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);

            return result;
        }
        #endregion

        #region Errorアクション
        [Fact]
        public IActionResult Error_ReturnsAViewResult_WithError()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ErrorViewModel>(viewResult.Model);
            Assert.Null(viewResult.ViewName);

            return result;
        }
        #endregion

    }
}