using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AddressManagement.Data;

namespace AddressManagement.Models.AddressViewModels
{
    public class AddressViewModel
    {
        public string AddressID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "タイトル")]
        public string Title { get; set; }

        [StringLength(7)]
        [Display(Name = "郵便番号")]
        public string PostalCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "都道府県")]
        public string Prefectures { get; set; }

        [MaxLength(50)]
        [Display(Name = "市区町村")]
        public string Ctiy { get; set; }

        [MaxLength(50)]
        [Display(Name ="番地")]
        public string Block { get; set; }

        [MaxLength(50)]
        [Display(Name = "建物")]
        public string Building { get; set; }

        [MaxLength(50)]
        [Display(Name = "備考")]
        public string Remarks { get; set; }

        public String UpdateTime { get; set; }

        public string FullAddress
        {
            get { return Prefectures + Ctiy + Block + Building; }
        }

        public Boolean IsUpdate { get; set; }

        ///// <summary>
        ///// キーを元にDBから情報を取得する
        ///// </summary>
        ///// <param name="addressID"></param>
        ///// <param name="context"></param>
        //public void SetData(int addressID, ApplicationDbContext context)
        //{
        //    var item = context.Addresses.SingleOrDefault(x => x.AddressID == addressID);
        //    if(item != null)
        //    {
        //        AddressID = item.AddressID.ToString();
        //        Title = item.Title;
        //        PostalCode = item.PostalCode?.ToString();
        //        Prefectures = item.Prefectures;
        //        Ctiy = item.Ctiy;
        //        Block = item.Block;
        //        Building = item.Building;
        //        Remarks = item.Remarks;
        //        IsUpdate = true;
        //    }
        //}

        /// <summary>
        /// モデルの情報でDBに登録する
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="now"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        //public int Insert(string userID, DateTime now, ApplicationDbContext context)
        //{
        //    var item = new Data.DbModel.Address()
        //    {
        //        Block = this.Block,
        //        Building = this.Building,
        //        Ctiy = this.Ctiy,
        //        PostalCode = ConvertPostalCode(),
        //        Prefectures = this.Prefectures,
        //        RegistrationTime = now,
        //        Remarks = this.Remarks,
        //        Title = this.Title,
        //        UpdateTime = now,
        //        UserID = userID
        //    };
        //    context.Addresses.Add(item);
        //    var ret = context.SaveChanges();

        //    // 画面を更新モードに変更する
        //    this.AddressID = item.AddressID.ToString();
        //    this.IsUpdate = true;

        //    return ret;
        //}

        /// <summary>
        /// モデルの情報でDBを更新する
        /// </summary>
        /// <param name="addressID"></param>
        /// <param name="now"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        //public int Update(DateTime now, ApplicationDbContext context)
        //{
        //    var addressID = int.Parse(this.AddressID);
        //    var item = context.Addresses.SingleOrDefault(x => x.AddressID == addressID);
        //    if (item != null)
        //    {
        //        item.Block = this.Block;
        //        item.Building = this.Building;
        //        item.Ctiy = this.Ctiy;
        //        item.PostalCode = ConvertPostalCode();
        //        item.Prefectures = this.Prefectures;
        //        item.Remarks = this.Remarks;
        //        item.Title = this.Title;
        //        item.UpdateTime = now;
        //    }

        //    return context.SaveChanges();
        //}

        /// <summary>
        /// 郵便番号を変換する
        /// </summary>
        /// <returns></returns>
        private int? ConvertPostalCode()
        {
            int? result = null;
            if (!string.IsNullOrWhiteSpace(this.PostalCode))
            {
                int zip;
                if (int.TryParse(this.PostalCode, out zip))
                {
                    result = zip;
                }
            }

            return result;
        }

    }
}
