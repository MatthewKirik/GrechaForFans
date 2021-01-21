using AutoMapper;
using DAL.Models;
using DataTransfer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
    public class ShopsRepository : IShopsRepository
    {
        IMapper mapper;
        public ShopsRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

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

        public async Task<List<ShopDto>> GetShops()
        {
            using (var db = new BuckwheatContext())
            {
                var shops = await db.Shops.ToListAsync();
                return mapper.Map<List<Shop>, List<ShopDto>>(shops);
            }
        }
    }
}
