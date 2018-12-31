using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressManagement.Data.DbModel
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
        public int AddressID { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        [StringLength(50)]
        [Column(Order = 2)]
        public string Title { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        [StringLength(7)]
        [Column(Order = 3)]
        public int? PostalCode { get; set; }

        /// <summary>
        /// 都道府県
        /// </summary>
        [StringLength(10)]
        [Column(Order = 4)]
        public string Prefectures { get; set; }

        /// <summary>
        /// 市区町村
        /// </summary>
        [StringLength(200)]
        [Column(Order = 5)]
        public string Ctiy { get; set; }

        /// <summary>
        /// 番地
        /// </summary>
        [StringLength(200)]
        [Column(Order = 6)]
        public string Block { get; set; }

        /// <summary>
        /// 建物
        /// </summary>
        [StringLength(200)]
        [Column(Order = 7)]
        public string Building { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [StringLength(200)]
        [Column(Order = 8)]
        public string Remarks { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        [Column(Order = 9)]
        public string UserID { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        [Column(Order = 10)]
        public DateTime? RegistrationTime { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        [Column(Order = 11)]
        public DateTime? UpdateTime { get; set; }

    }
}
