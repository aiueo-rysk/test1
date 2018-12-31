using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressManagement.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0}は必須入力です。")]
        [EmailAddress(ErrorMessage = "{0}の形式が不正です。")]
        [Display(Name = "メールアドレス")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}は必須入力です。")]
        [StringLength(100, ErrorMessage = "{0} は{2}～{1}桁で入力してください。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "パスワード確認用")]
        [Compare("Password", ErrorMessage = "パスワードと{0}が一致しません。")]
        public string ConfirmPassword { get; set; }
    }
}
