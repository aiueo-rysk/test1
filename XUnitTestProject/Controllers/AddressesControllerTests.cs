using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AddressManagement.Controllers;
using AddressManagement.Models;
using Xunit;
using AddressManagement.Core.Interfaces;

namespace TestingControllersSample.Tests.UnitTests
{
    public class AddressesControllerTests
    {

        #region Indexメソッド(Get)
        [Fact]
        public async Task Index_Get()
        {
            // Arrange
            var testUserId = "testUserId";
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.GetAddressListAsync(testUserId))
                    .ReturnsAsync((List<Address>)null);
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<Address>>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);

        }
        #endregion

        #region Searchメソッド(Post)
        [Fact]
        public async Task Search_Post()
        {
            string searchTitle = "searchTitle";
            string searchPostalCode = "searchPostalCode";
            string searchPrefectures = "searchPrefectures";
            string searchCtiy = "searchCtiy";
            string searchBlock = "searchBlock";
            string searchBuilding = "searchBuilding";
            string searchRemarks = "searchRemarks";

            // Arrange
            var testUserId = "testUserId";
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.SearchAsync(testUserId, searchTitle, searchPostalCode, searchPrefectures, searchCtiy, searchBlock, searchBuilding, searchRemarks))
                    .ReturnsAsync((List<Address>)null);
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Search(searchTitle, searchPostalCode, searchPrefectures, searchCtiy, searchBlock, searchBuilding, searchRemarks);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<Address>>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewData.Model);
            Assert.Equal("Index", viewResult.ViewName);

        }
        #endregion

        #region Detailsメソッド(Get) 正常系
        [Fact]
        public async Task Details_Get_Normal()
        {
            // Arrange
            var testAddressId = 1;
            var address = GetSampleAddressObject();
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testAddressId))
                    .ReturnsAsync(address);
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Details(testAddressId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Address>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);

        }
        #endregion

        #region Detailsメソッド(Get) 異常系①
        [Fact]
        public async Task Details_Get_Abnormal1()
        {
            // Arrange
            int? testAddressId = null;
            var mockRepo = new Mock<IAddressesRepository>();
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Details(testAddressId);

            // Assert
            var badRequestResult = Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Detailsメソッド(Get) 異常系②
        [Fact]
        public async Task Details_Get_Abnormal2()
        {
            // Arrange
            var testAddressId = 1;
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testAddressId))
                    .ReturnsAsync((Address)null);
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Details(testAddressId);

            // Assert
            var badRequestResult = Assert.IsType<NotFoundResult>(result);

        }
        #endregion

        #region Createメソッド(Get)
        [Fact]
        public IActionResult Create_Get()
        {
            // Arrange
            var mockRepo = new Mock<IAddressesRepository>();
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);

            return result;

        }
        #endregion

        #region Createメソッド(Post) 正常系
        [Fact]
        public async Task Create_Post_Normal()
        {
            // Arrange
            var address = GetSampleAddressObject();
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.AddAsync(address))
                    .Returns(Task.CompletedTask)
                    .Verifiable();
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Create(address);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
        #endregion

        #region Createメソッド(Post) 異常系
        [Fact]
        public async Task Create_Post_Abnormal1()
        {
            // Arrange
            var address = GetSampleAddressObject();
            var mockRepo = new Mock<IAddressesRepository>();
            var controller = new AddressesController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Create(address);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Address>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);
        }
        #endregion

        #region Editメソッド(Get) 正常系
        [Fact]
        public async Task Edit_Get_Normal()
        {
            // Arrange
            var testAddressId = 1;
            var address = GetSampleAddressObject();
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testAddressId))
                    .ReturnsAsync(address);
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Edit(testAddressId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Address>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);
        }
        #endregion

        #region Editメソッド(Get) 異常系①
        [Fact]
        public async Task Edit_Get_Abnormal1()
        {
            // Arrange
            int? testAddressId = null;
            var mockRepo = new Mock<IAddressesRepository>();
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Edit(testAddressId);

            // Assert
            var badRequestResult = Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Editメソッド(Get) 異常系②
        [Fact]
        public async Task Edit_Get_Abnormal2()
        {
            // Arrange
            var testAddressId = 1;
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testAddressId))
                    .ReturnsAsync((Address)null);
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Edit(testAddressId);

            // Assert
            var badRequestResult = Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Editメソッド(Post) 正常系
        [Fact]
        public async Task Edit_Post_Normal()
        {
            // Arrange
            var address = GetSampleAddressObject();
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.UpdateAsync(address))
                    .Returns(Task.CompletedTask)
                    .Verifiable();
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Edit(address);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
        #endregion

        #region Editメソッド(Post) 異常系
        [Fact]
        public async Task Edit_Post_Abnormal()
        {
            // Arrange
            var address = GetSampleAddressObject();
            var mockRepo = new Mock<IAddressesRepository>();
            var controller = new AddressesController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Edit(address);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Address>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);
        }
        #endregion

        #region Deleteメソッド(Get) 正常系
        [Fact]
        public async Task Delete_Get_Normal()
        {
            // Arrange
            var testAddressId = 1;
            var address = GetSampleAddressObject();
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testAddressId))
                    .ReturnsAsync(address);
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Delete(testAddressId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Address>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);
        }
        #endregion

        #region Deleteメソッド(Get) 異常系①
        [Fact]
        public async Task Delete_Get_Abnormal1()
        {
            // Arrange
            int? testAddressId = null;
            var mockRepo = new Mock<IAddressesRepository>();
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Delete(testAddressId);

            // Assert
            var badRequestResult = Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Deleteメソッド(Get) 異常系②
        [Fact]
        public async Task Delete_Get_Abnormal2()
        {
            // Arrange
            var testAddressId = 1;
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testAddressId))
                    .ReturnsAsync((Address)null);
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.Delete(testAddressId);

            // Assert
            var badRequestResult = Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region DeleteConfirmedメソッド(Post
        [Fact]
        public async Task DeleteConfirmed_Post()
        {
            // Arrange
            var testAddressId = 1;
            var mockRepo = new Mock<IAddressesRepository>();
            mockRepo.Setup(repo => repo.RemoveAsync(testAddressId))
                    .Returns(Task.CompletedTask)
                    .Verifiable();
            var controller = new AddressesController(mockRepo.Object);

            // Act
            var result = await controller.DeleteConfirmed(testAddressId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
        #endregion


        private Address GetSampleAddressObject()
        {
            return new Address()
            {
                AddressID = 1,
                Title = "テスト住所１",
                PostalCode = 1234567,
                Prefectures = "",
                Ctiy = "",
                Block = "",
                Building = "",
                Remarks = ""
            };
        }

    }
}