using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0}は必須入力です。")]
        [EmailAddress]
        [Display(Name = "メールアドレス")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}は必須入力です。")]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; }

        [Display(Name = "パスワードを記憶する")]
        public bool RememberMe { get; set; }
    }
}
