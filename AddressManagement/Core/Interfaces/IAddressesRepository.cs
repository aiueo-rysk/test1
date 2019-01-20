using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressManagement.Models;

namespace AddressManagement.Core.Interfaces
{
    /// <summary>
    /// 住所情報操作インターフェース
    /// </summary>
    public interface IAddressesRepository
    {
        Task<Address> GetByIdAsync(int id);

        Task<List<Address>> GetAddressListAsync(string userId);

        Task<List<Address>> SearchAsync(string userId, 
                                        string searchTitle,
                                        string searchPostalCode,
                                        string searchPrefectures,
                                        string searchCtiy,
                                        string searchBlock,
                                        string searchBuilding,
                                        string searchRemarks);

        Task AddAsync(Address address);

        Task UpdateAsync(Address address);

        Task RemoveAsync(int id);
    }
}
