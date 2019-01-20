using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using AddressManagement.Models;
using AddressManagement.Models.AccountViewModels;

namespace AddressManagement.Core.Interfaces
{
    /// <summary>
    /// アカウント情報操作インターフェース
    /// </summary>
    public interface IAccountRepository
    {
        Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(LoginViewModel model);

        Task SignOutAsync();

        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);

        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();

        Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync(ExternalLoginInfo info);

        Task<IdentityResult> CreateAsync(ApplicationUser user);

        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

        Task<IdentityResult> AddLoginAsync(ApplicationUser user, ExternalLoginInfo info);

        Task SignInAsync(ApplicationUser user);
    }
}
