using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressManagement.Core.Interfaces;
using AddressManagement.Data;
using AddressManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EfAddressesRepository : IAddressesRepository
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext"></param>
        public EfAddressesRepository(ApplicationDbContext dbContext)
        {
            // DI情報を取得する
            _dbContext = dbContext;
        }

        /// <summary>
        /// 住所IDをキーに住所情報を1件取得する
        /// </summary>
        /// <param name="id">住所ID</param>
        /// <returns>住所情報オブジェクト</returns>
        public Task<Address> GetByIdAsync(int id)
        {
            return _dbContext.Address.SingleOrDefaultAsync(x => x.AddressID.Equals(id));
        }

        /// <summary>
        /// ログインユーザーが作成した住所情報を全件取得する
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <returns>住所情報リスト</returns>
        public Task<List<Address>> GetAddressListAsync(string userId)
        {
            return _dbContext.Address.Where(x => x.UserID.Equals(userId)).ToListAsync();
        }

        /// <summary>
        /// 入力した条件で絞り込み（部分一致）検索を行う
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="searchTitle">検索条件_タイトル</param>
        /// <param name="searchPostalCode">検索条件_郵便番号</param>
        /// <param name="searchPrefectures">検索条件_都道府県</param>
        /// <param name="searchCtiy">検索条件_市区町村</param>
        /// <param name="searchBlock">検索条件_番地</param>
        /// <param name="searchBuilding">検索条件_建物</param>
        /// <param name="searchRemarks">検索条件_備考</param>
        /// <returns>住所情報リスト</returns>
        public Task<List<Address>> SearchAsync(string userId,
                                                string searchTitle,
                                                string searchPostalCode,
                                                string searchPrefectures,
                                                string searchCtiy,
                                                string searchBlock,
                                                string searchBuilding,
                                                string searchRemarks)
        {
            var query = _dbContext.Address.Where(x => x.UserID.Equals(userId));

            // 検索条件を反映
            if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                query = query.Where(x => x.Title.Contains(searchTitle));
            }
            if (!string.IsNullOrWhiteSpace(searchPostalCode))
            {
                query = query.Where(x => x.PostalCode.ToString().Contains(searchPostalCode));
            }
            if (!string.IsNullOrWhiteSpace(searchPrefectures))
            {
                query = query.Where(x => x.Prefectures.Contains(searchPrefectures));
            }
            if (!string.IsNullOrWhiteSpace(searchCtiy))
            {
                query = query.Where(x => x.Ctiy.Contains(searchCtiy));
            }
            if (!string.IsNullOrWhiteSpace(searchBlock))
            {
               query = query.Where(x => x.Block.Contains(searchBlock));
            }
            if (!string.IsNullOrWhiteSpace(searchBuilding))
            {
                query = query.Where(x => x.Building.Contains(searchBuilding));
            }
            if (!string.IsNullOrWhiteSpace(searchRemarks))
            {
                query = query.Where(x => x.Remarks.Contains(searchRemarks));
            }

            // 並び替え
            query = query.OrderByDescending(x => x.UpdateTime);

            return query.ToListAsync();
        }

        /// <summary>
        /// 住所情報を登録する
        /// </summary>
        /// <param name="address">住所情報オブジェクト</param>
        /// <returns></returns>
        public Task AddAsync(Address address)
        {
            _dbContext.Address.Add(address);
            return _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 住所情報を更新する
        /// </summary>
        /// <param name="address">住所情報オブジェクト</param>
        /// <returns></returns>
        public Task UpdateAsync(Address address)
        {
            _dbContext.Entry(address).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 住所情報を削除する
        /// </summary>
        /// <param name="id">住所ID</param>
        /// <returns></returns>
        public Task RemoveAsync(int id) {
            var address = _dbContext.Address.SingleOrDefault(m => m.AddressID == id);
            _dbContext.Address.Remove(address);
            return _dbContext.SaveChangesAsync();
        }
    }
}
