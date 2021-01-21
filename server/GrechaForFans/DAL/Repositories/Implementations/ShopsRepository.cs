using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
    public class ShopsRepository : IShopsRepository
    {
        public Task AddShop(ShopDto shopData)
        {
            throw new NotImplementedException();
        }

        public Task<ShopDto> GetShop(int shopId)
        {
            throw new NotImplementedException();
        }

        public Task<ShopDto> GetShop(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<ShopDto>> GetShops()
        {
            throw new NotImplementedException();
        }
    }
}
