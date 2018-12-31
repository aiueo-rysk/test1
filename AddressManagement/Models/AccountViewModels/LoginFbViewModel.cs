using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressManagement.Models.AccountViewModels
{
    public class LoginFbViewModel
    {
        [Required(ErrorMessage = "{0}は必須入力です。")]
        [Display(Name = "ユーザーID")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "{0}は必須入力です。")]
        [Display(Name = "メールアドレス")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}は必須入力です。")]
        [Display(Name = "ユーザー名")]
        public string Name { get; set; }
    }
}
