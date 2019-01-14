using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AddressManagement.Data;
using AddressManagement.Models;

namespace AddressManagement.Models.AddressViewModels
{
    public class AddressListViewModel
    {

        [Display(Name = "タイトル")]
        public string Title { get; set; }

        [Display(Name = "郵便番号")]
        public string PostalCode { get; set; }

        [Display(Name = "都道府県")]
        public string Prefectures { get; set; }

        [Display(Name = "市区町村")]
        public string Ctiy { get; set; }

        [Display(Name = "番地")]
        public string Block { get; set; }

        [Display(Name = "建物")]
        public string Building { get; set; }

        [Display(Name = "備考")]
        public string Remarks { get; set; }

        [Display(Name = "更新日時")]
        public string UpdateTime { get; set; }

        public List<AddressViewModel> AddressList { get; set; } = new List<AddressViewModel>();

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="context"></param>
        //public int Search(string userID, ApplicationDbContext context)
        //{
        //    // 自分が登録したデータだけを表示する
        //    var query = context.Addresses.Where(x => x.UserID == userID);

        //    // 検索条件を反映
        //    if (!String.IsNullOrWhiteSpace(Title))
        //    {
        //        query = query.Where(x => x.Title.Contains(Title));
        //    }
        //    if (!String.IsNullOrWhiteSpace(PostalCode))
        //    {
        //        query = query.Where(x => x.PostalCode.ToString().Contains(PostalCode));
        //    }
        //    if (!String.IsNullOrWhiteSpace(Prefectures))
        //    {
        //        query = query.Where(x => x.Prefectures.Contains(Prefectures));
        //    }
        //    if (!String.IsNullOrWhiteSpace(Ctiy))
        //    {
        //        query = query.Where(x => x.Ctiy.Contains(Ctiy));
        //    }
        //    if (!String.IsNullOrWhiteSpace(Block))
        //    {
        //        query = query.Where(x => x.Block.Contains(Block));
        //    }
        //    if (!String.IsNullOrWhiteSpace(Building))
        //    {
        //        query = query.Where(x => x.Building.Contains(Building));
        //    }
        //    if (!String.IsNullOrWhiteSpace(Remarks))
        //    {
        //        query = query.Where(x => x.Remarks.Contains(Remarks));
        //    }

        //    var items = query.ToList();
        //    if(items.Count() > 0)
        //    {
        //        // 並び替え
        //        items = items.OrderByDescending(x => x.UpdateTime).ToList();

        //        AddressList = new List<AddressViewModel>();    
        //        foreach (var i in items)
        //        {
        //            AddressList.Add(new AddressViewModel()
        //            {
        //                AddressID = i.AddressID.ToString(),
        //                Block = i.Block,
        //                Building = i.Building,
        //                Ctiy = i.Ctiy,
        //                PostalCode = i.PostalCode?.ToString(),
        //                Prefectures = i.Prefectures,
        //                Remarks = i.Remarks,
        //                Title = i.Title,
        //                UpdateTime = i.UpdateTime?.ToString("yyyy/MM/dd HH:mm:ss")
        //            });
        //        }
        //    }

        //    return items.Count;
        //}


        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="addressID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        //public int Delete(int addressID, ApplicationDbContext context)
        //{
        //    Address item = context.Addresses.SingleOrDefault(x => x.AddressID == addressID);
        //    context.Addresses.Remove(item);
        //    return context.SaveChanges();
        //}
        

    }
}
