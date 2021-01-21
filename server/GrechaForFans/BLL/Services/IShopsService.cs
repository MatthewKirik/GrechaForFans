using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IShopsService
    {
        Task<List<ShopDto>> GetShops();
        Task<ShopDto> GetShop(int shopId);
    }
}
