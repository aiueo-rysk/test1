using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressManagement.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required(ErrorMessage = "{0}は必須入力です。")]
        [EmailAddress(ErrorMessage = "{0}の形式が不正です。")]
        [Display(Name = "メールアドレス")]
        public string Email { get; set; }
    }
}
