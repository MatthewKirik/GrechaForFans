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

        public async Task AddShop(ShopDto shopData)
        {
            using (var db = new BuckwheatContext())
            {
                Shop shop = mapper.Map<ShopDto, Shop>(shopData);
                shop.Lots = null;
                await db.Shops.AddAsync(shop);
                await db.SaveChangesAsync();
            }
        }

        public async Task<ShopDto> GetShop(int shopId)
        {
            using (var db = new BuckwheatContext())
            {
                var shop = await db.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
                return mapper.Map<Shop, ShopDto>(shop);
            }
        }

        public async Task<ShopDto> GetShop(string name)
        {
            using (var db = new BuckwheatContext())
            {
                var shop = await db.Shops.FirstOrDefaultAsync(x => x.Name == name);
                return mapper.Map<Shop, ShopDto>(shop);
            }
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
