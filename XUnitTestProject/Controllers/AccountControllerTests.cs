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
using AddressManagement.Models.AccountViewModels;
using AddressManagement.Core.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace TestingControllersSample.Tests.UnitTests
{
    public class AccountControllerTests
    {
        #region Loginメソッド(Get)
        [Fact]
        public async Task Login_Get()
        {
            // Arrange
            string returnUrl = null;
            var mockRepo = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.Login(returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);

        }
        #endregion

        #region Loginメソッド(Post) 正常系　ログインに成功してリダイレクト
        [Fact]
        public async Task Login_Post_Normal()
        {
            // Arrange
            string returnUrl = null;
            var loginViewModel = new LoginViewModel();
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.PasswordSignInAsync(loginViewModel))
                    .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.Login(loginViewModel, returnUrl);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        #endregion

        #region Loginメソッド(Post) 異常系①　ログインに失敗する
        [Fact]
        public async Task Login_Post_Abnormal1()
        {
            // Arrange
            string returnUrl = null;
            var loginViewModel = new LoginViewModel();
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.PasswordSignInAsync(loginViewModel))
                    .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.Login(loginViewModel, returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<LoginViewModel>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);
        }
        #endregion

        #region Loginメソッド(Post) 異常系②　モデルエラー
        [Fact]
        public async Task Login_Post_Abnormal2()
        {
            // Arrange
            string returnUrl = null;
            var loginViewModel = new LoginViewModel();
            var mockRepo = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Login(loginViewModel, returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<LoginViewModel>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);
        }
        #endregion


        #region Registerメソッド(Get)
        [Fact]
        public IActionResult Register_Get()
        {
            // Arrange
            string returnUrl = null;
            var mockRepo = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = controller.Register(returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);

            return result;
        }
        #endregion

        #region Registerメソッド(Post) 正常系　登録に成功してリダイレクト
        [Fact]
        public async Task Register_Post_Normal()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel();
            string returnUrl = null;
            var user = new ApplicationUser { UserName = registerViewModel.Email, Email = registerViewModel.Email };
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.CreateAsync(user, registerViewModel.Password))
                    .ReturnsAsync(IdentityResult.Success);
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.Register(registerViewModel, returnUrl);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        #endregion


        #region Registerメソッド(Post) 異常系①　登録に失敗する
        [Fact]
        public async Task Register_Post_Abnormal1()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel();
            string returnUrl = null;
            var user = new ApplicationUser { UserName = registerViewModel.Email, Email = registerViewModel.Email };
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.CreateAsync(user, registerViewModel.Password))
                    .ReturnsAsync(IdentityResult.Failed(null));
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.Register(registerViewModel, returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<RegisterViewModel>(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);

        }
        #endregion

        #region Registerメソッド(Post) 異常系②　モデルエラー
        [Fact]
        public IActionResult Register_Post_Abnormal2()
        {
            // Arrange
            string returnUrl = null;
            var mockRepo = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = controller.Register(returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);

            return result;
        }
        #endregion


        #region Logoutメソッド(Post)
        [Fact]
        public async Task Logout_Post()
        {
            // Arrange
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.SignOutAsync())
                    .Returns(Task.CompletedTask)
                    .Verifiable();
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.Logout();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
        #endregion

        #region ExternalLoginメソッド(Post) 正常系
        [Fact]
        public IActionResult ExternalLogin_Post()
        {
            // Arrange
            string provider = null;
            string returnUrl = null;
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.ConfigureExternalAuthenticationProperties(provider, returnUrl))
                   .Returns(new AuthenticationProperties());
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = controller.ExternalLogin(provider, returnUrl);

            // Assert
            var challengeResult = Assert.IsType<ChallengeResult>(result);

            return result;
        }
        #endregion

        #region ExternalLoginCallbackメソッド(Get) 正常系　ExternalLoginSignInAsyncの戻り値がSucceeded
        [Fact]
        public async Task ExternalLoginCallback_Get_Normal()
        {
            // Arrange
            string returnUrl = null;
            string remoteError = null;
            var externalLoginInfo = GetSampleExternalLoginInfoObject();
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.GetExternalLoginInfoAsync())
                    .ReturnsAsync(externalLoginInfo);
            mockRepo.Setup(repo => repo.ExternalLoginSignInAsync(externalLoginInfo))
                    .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.ExternalLoginCallback(returnUrl, remoteError);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        #endregion

        #region ExternalLoginCallbackメソッド(Get) 異常系①　remoteErrorがnull以外
        [Fact]
        public async Task ExternalLoginCallback_Get_Abnormal1()
        {
            // Arrange
            string returnUrl = null;
            string remoteError = "null以外";
            var mockRepo = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepo.Object);
            
            // Act
            var result = await controller.ExternalLoginCallback(returnUrl, remoteError);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Login", redirectToActionResult.ActionName);
        }
        #endregion

        #region ExternalLoginCallbackメソッド(Get) 異常系②　GetExternalLoginInfoAsyncの戻り値がnull
        [Fact]
        public async Task ExternalLoginCallback_Get_Abnormal2()
        {
            // Arrange
            string returnUrl = null;
            string remoteError = null;
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.GetExternalLoginInfoAsync())
                    .ReturnsAsync((ExternalLoginInfo)null);
            var controller = new AccountController(mockRepo.Object);
            
            // Act
            var result = await controller.ExternalLoginCallback(returnUrl, remoteError);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Login", redirectToActionResult.ActionName);
        }
        #endregion

        #region ExternalLoginCallbackメソッド(Get) 異常系③　ExternalLoginSignInAsyncの戻り値がLockedOut
        [Fact]
        public async Task ExternalLoginCallback_Get_Abnormal3()
        {
            // Arrange
            string returnUrl = null;
            string remoteError = null;
            var externalLoginInfo = GetSampleExternalLoginInfoObject();
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.GetExternalLoginInfoAsync())
                    .ReturnsAsync(externalLoginInfo);
            mockRepo.Setup(repo => repo.ExternalLoginSignInAsync(externalLoginInfo))
                    .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.LockedOut);
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.ExternalLoginCallback(returnUrl, remoteError);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Lockout", redirectToActionResult.ActionName);
        }
        #endregion

        #region ExternalLoginCallbackメソッド(Get) 異常系④　ExternalLoginSignInAsyncの戻り値がSucceeded/LockedOut以外
        [Fact]
        public async Task ExternalLoginCallback_Get_Abnormal4()
        {
            // Arrange
            string returnUrl = null;
            string remoteError = null;
            var externalLoginInfo = GetSampleExternalLoginInfoObject();
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.GetExternalLoginInfoAsync())
                    .ReturnsAsync(externalLoginInfo);
            mockRepo.Setup(repo => repo.ExternalLoginSignInAsync(externalLoginInfo))
                    .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.NotAllowed);
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.ExternalLoginCallback(returnUrl, remoteError);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ExternalLoginViewModel>(viewResult.ViewData.Model);
            Assert.Equal("ExternalLogin", viewResult.ViewName);
        }
        #endregion

        #region ExternalLoginConfirmationメソッド(Post) 正常系　ログインに成功し、リダイレクトする
        [Fact]
        public async Task ExternalLoginConfirmation_Post_Normal()
        {
            // Arrange
            var externalLoginViewModel = new ExternalLoginViewModel() { Email = "testEmail" };
            string provider = null;
            string returnUrl = null;
            var externalLoginInfo = GetSampleExternalLoginInfoObject();
            var user = new ApplicationUser { UserName = externalLoginViewModel.Email, Email = externalLoginViewModel.Email };
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(repo => repo.GetExternalLoginInfoAsync())
                    .ReturnsAsync(externalLoginInfo);

            mockRepo.Setup(repo => repo.CreateAsync(user))
                    .ReturnsAsync(IdentityResult.Success);

            mockRepo.Setup(repo => repo.AddLoginAsync(user, externalLoginInfo))
                    .ReturnsAsync(IdentityResult.Success);

            mockRepo.Setup(repo => repo.SignInAsync(user))
                    .Returns(Task.CompletedTask)
                    .Verifiable();

            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = await controller.ExternalLoginConfirmation(externalLoginViewModel, provider, returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region ExternalLoginConfirmationメソッド(Post) 異常系①　モデルエラー
        [Fact]
        public async Task ExternalLoginConfirmation_Post_Abnormal1()
        {
            // Arrange
            var externalLoginViewModel = new ExternalLoginViewModel();
            string provider = null;
            string returnUrl = null;
            var mockRepo = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.ExternalLoginConfirmation(externalLoginViewModel, provider, returnUrl);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ExternalLoginViewModel>(viewResult.ViewData.Model);
            Assert.Equal("ExternalLogin", viewResult.ViewName);
        }
        #endregion

        #region Lockoutメソッド(Get)
        [Fact]
        public IActionResult Lockout_Get()
        {
            // Arrange
            var mockRepo = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = controller.Lockout();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
            Assert.Null(viewResult.ViewName);

            return result;
        }
        #endregion

        private ExternalLoginInfo GetSampleExternalLoginInfoObject()
        {
            var principal = new ClaimsPrincipal();
            var loginProvider = string.Empty;
            var providerKey = string.Empty;
            var displayNeme = string.Empty;

            return new ExternalLoginInfo(principal, loginProvider, providerKey, displayNeme);
        }

    }
}