using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressManagement.Core.Interfaces;
using AddressManagement.Models;
using AddressManagement.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public class EfAccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public EfAccountRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            // DI情報を取得する
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// ログイン画面の入力情報でログインを試みる
        /// </summary>
        /// <param name="model">rログインビューモデル</param>
        /// <returns>ログイン成功／失敗</returns>
        public Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(LoginViewModel model)
        {
            return _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }

        /// <summary>
        /// ログイン状態を解除する
        /// </summary>
        /// <returns></returns>
        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        /// <summary>
        /// 外部認証サービス情報を取得する
        /// </summary>
        /// <param name="provider">プロバイダー名</param>
        /// <param name="redirectUrl">リダイレクトURL</param>
        /// <returns>外部認証サービスオブジェクト</returns>
        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        /// <summary>
        /// 外部サービスによる認証を試みる
        /// </summary>
        /// <returns>外部認証ログイン情報</returns>
        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return _signInManager.GetExternalLoginInfoAsync();
        }

        /// <summary>
        /// 外部サービスの認証情報を利用して、ログインを試みる
        /// </summary>
        /// <param name="info">外部認証ログイン情報</param>
        /// <returns>ログイン成功／失敗</returns>
        public Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync(ExternalLoginInfo info)
        {
            return _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        }

        /// <summary>
        /// ユーザー情報を作成する（DB登録）
        /// </summary>
        /// <param name="user">アプリケーションユーザーオブジェクト</param>
        /// <returns>ユーザー情報作成成功／失敗</returns>
        public Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            return _userManager.CreateAsync(user);
        }

        /// <summary>
        /// ユーザー情報を作成する（DB登録）
        /// </summary>
        /// <param name="user">アプリケーションユーザーオブジェクト</param>
        /// <param name="password">パスワード</param>
        /// <returns>ユーザー情報作成成功／失敗</returns>
        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        /// <summary>
        /// 外部認証情報からユーザー情報を作成する（DB登録）
        /// </summary>
        /// <param name="user">アプリケーションユーザーオブジェクト</param>
        /// <param name="info">外部認証ログイン情報</param>
        /// <returns>ユーザー情報作成成功／失敗</returns>
        public Task<IdentityResult> AddLoginAsync(ApplicationUser user, ExternalLoginInfo info)
        {
            return _userManager.AddLoginAsync(user, info);
        }

        /// <summary>
        /// ログインを行い、認証情報を作成する
        /// </summary>
        /// <param name="user">アプリケーションユーザーオブジェクト</param>
        /// <returns></returns>
        public Task SignInAsync(ApplicationUser user)
        {
            return _signInManager.SignInAsync(user, isPersistent: false);
        }

    }
}
