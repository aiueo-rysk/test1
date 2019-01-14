using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressManagement.Models
{
    /// <summary>
    /// 住所テーブル
    /// </summary>
    public class Address
    {
        /// <summary>
        /// 住所ID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [Display(Name = "住所ID")]
        public int AddressID { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        [StringLength(50, ErrorMessage ="{0}は{1}文字以内で入力してください。")]
        [Column(Order = 2)]
        [Display(Name = "タイトル")]
        [Required(ErrorMessage = "{0}は必須入力です。")]
        public string Title { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        [Column(Order = 3)]
        [Display(Name = "郵便番号")]
        public int? PostalCode { get; set; }

        /// <summary>
        /// 都道府県
        /// </summary>
        [StringLength(10, ErrorMessage = "{0}は{1}文字以内で入力してください。")]
        [Column(Order = 4)]
        [Display(Name = "都道府県")]
        public string Prefectures { get; set; }

        /// <summary>
        /// 市区町村
        /// </summary>
        [StringLength(200, ErrorMessage = "{0}は{1}文字以内で入力してください。")]
        [Column(Order = 5)]
        [Display(Name = "市区町村")]
        public string Ctiy { get; set; }

        /// <summary>
        /// 番地
        /// </summary>
        [StringLength(200, ErrorMessage = "{0}は{1}文字以内で入力してください。")]
        [Column(Order = 6)]
        [Display(Name = "番地")]
        public string Block { get; set; }

        /// <summary>
        /// 建物
        /// </summary>
        [StringLength(200, ErrorMessage = "{0}は{1}文字以内で入力してください。")]
        [Column(Order = 7)]
        [Display(Name = "建物")]
        public string Building { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [StringLength(200, ErrorMessage = "{0}は{1}文字以内で入力してください。")]
        [Column(Order = 8)]
        [Display(Name = "備考")]
        public string Remarks { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        [Column(Order = 9)]
        [Display(Name = "ユーザーID")]
        public string UserID { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        [Column(Order = 10)]
        [Display(Name = "登録日時")]
        public DateTime? RegistrationTime { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        [Column(Order = 11)]
        [Display(Name = "更新日時")]
        public DateTime? UpdateTime { get; set; }

    }
}
