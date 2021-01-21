using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IShopsRepository
    {
        Task AddShop(ShopDto shopData);
        Task<List<ShopDto>> GetShops();
        Task<ShopDto> GetShop(int shopId);
        Task<ShopDto> GetShop(string name);
    }
}
