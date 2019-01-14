using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AddressManagement.Controllers;
using AddressManagement.Models;
using Xunit;
using AddressManagement.Data;

namespace TestingControllersSample.Tests.UnitTests
{
    public class AddressesControllerTests
    {

        private AddressesController _controller;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AddressesControllerTests()
        {
            Create();
        }

        /// <summary>
        /// インスタンス生成
        /// </summary>
        [Fact]
        public void Create()
        {
             ApplicationDbContext _context = null;

            _controller = new AddressesController(_context);
        }

        #region Index Get
        [Fact]
        public async Task Index_Get()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

        }
        #endregion

        #region Search Post
        [Fact]
        public async Task Search_Post()
        {
            string searchTitle = null;
            string searchPostalCode = null;
            string searchPrefectures = null;
            string searchCtiy = null;
            string searchBlock = null;
            string searchBuilding = null;
            string searchRemarks = null;

            // Act
            var result = await _controller.Search(searchTitle, searchPostalCode, searchPrefectures, searchCtiy, searchBlock, searchBuilding, searchRemarks);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

        }
        #endregion

        #region Details Get
        [Fact]
        public async Task Details_Get()
        {
            int? id = null;

            // Act
            var result = await _controller.Details(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

        }
        #endregion

        #region Create Get
        [Fact]
        public IActionResult Create_Get()
        {
            // Act
            var result = _controller.Create();

            return result;

        }
        #endregion

        #region Create Post
        [Fact]
        public async Task Create_Post()
        {
            Address address = null;

            // Act
            var result = await _controller.Create(address);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region Edit Get
        [Fact]
        public async Task Edit_Get()
        {
            int? id = null;

            // Act
            var result = await _controller.Edit(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region Edit Post
        [Fact]
        public async Task Edit_Post()
        {
            Address address = null;

            // Act
            var result = await _controller.Edit(address);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region Delete Get
        [Fact]
        public async Task Delete_Get()
        {
            int? id = null;

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region DeleteConfirmed Post
        [Fact]
        public async Task DeleteConfirmed_Post()
        {
            int addressID = 0;

            // Act
            var result = await _controller.DeleteConfirmed(addressID);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion
    }
}