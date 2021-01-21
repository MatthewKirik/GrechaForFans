using DAL.Repositories;
using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class ShopsService : IShopsService
    {
        IShopsRepository shopsRepository;
        
        public ShopsService(IShopsRepository shopsRepository)
        {
            this.shopsRepository = shopsRepository;
        }

        public Task<ShopDto> GetShop(int shopId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ShopDto>> GetShops() 
            => await shopsRepository.GetShops();
    }
}
