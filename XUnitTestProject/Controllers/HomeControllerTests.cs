using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AddressManagement.Controllers;
using Xunit;

namespace TestingControllersSample.Tests.UnitTests
{
    public class HomeControllerTests
    {
        #region Indexアクション
        [Fact]
        public IActionResult Index_ReturnsAViewResult()
        {
            var controller = new HomeController();

            var result = controller.Index();

            return result;

        }
        #endregion

        #region Errorアクション
        [Fact]
        public IActionResult Error_ReturnsAViewResult_WithError()
        {
            var controller = new HomeController();

            var result = controller.Error();

            return result;
        }
        #endregion

    }
}