using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AddressManagement.Controllers;
using AddressManagement.Models;
using Xunit;
using Microsoft.AspNetCore.Identity;
using AddressManagement.Services;
using Microsoft.Extensions.Logging;
using AddressManagement.Models.AccountViewModels;

namespace TestingControllersSample.Tests.UnitTests
{
    public class AccountControllerTests
    {

        private AccountController _controller;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AccountControllerTests()
        {
            Create();
        }

        /// <summary>
        /// インスタンス生成
        /// </summary>
        [Fact]
        public void Create()
        {
            UserManager<ApplicationUser> _userManager = null;
            SignInManager<ApplicationUser> _signInManager = null;
            IEmailSender _emailSender = null;
            ILogger<AccountController> _logger = null;

            _controller = new AccountController(_userManager, _signInManager, _emailSender, _logger);
        }

        #region Login Get
        [Fact]
        public async Task Login_Get()
        {
            string returnUrl = null;

            // Act
            var result = await _controller.Login(returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

        }
        #endregion

        #region Login Post
        [Fact]
        public async Task Login_Post()
        {
            LoginViewModel model = null;
            string returnUrl = null;

            // Act
            var result = await _controller.Login(model, returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion


        #region Register Get
        [Fact]
        public IActionResult Register_Get()
        {
            string returnUrl = null;

            // Act
            var result = _controller.Register(returnUrl);

            return result;
        }
        #endregion


        #region Register Post
        [Fact]
        public async Task Register_Post()
        {
            RegisterViewModel model = null;
            string returnUrl = null;

            // Act
            var result = await _controller.Register(model, returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion


        #region Logout Post
        [Fact]
        public async Task Logout_Post()
        {
            // Act
            var result = await _controller.Logout();

            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region ExternalLogin Post
        [Fact]
        public IActionResult ExternalLogin_Post()
        {
            string provider = null;
            string returnUrl = null;

            // Act
            var result = _controller.ExternalLogin(provider, returnUrl);

            return result;
        }
        #endregion

        #region ExternalLoginCallback Get
        [Fact]
        public async Task ExternalLoginCallback_Get()
        {
            string returnUrl = null;
            string remoteError = null;

            // Act
            var result = await _controller.ExternalLoginCallback(returnUrl, remoteError);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region ExternalLoginConfirmation Post
        [Fact]
        public async Task ExternalLoginConfirmation_Post()
        {
            ExternalLoginViewModel model = null;
            string provider = null;
            string returnUrl = null;

            // Act
            var result = await _controller.ExternalLoginConfirmation(model, provider, returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region Lockout Get
        [Fact]
        public IActionResult Lockout_Get()
        {
            var result = _controller.Lockout();

            return result;
        }
        #endregion
    }
}