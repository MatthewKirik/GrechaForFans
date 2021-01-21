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
    public class LotsRepository : ILotsRepository
    {
        IMapper mapper;
        public LotsRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task AddLot(LotDto lotData, int shopId)
        {
            using (var db = new BuckwheatContext())
            {
                Shop shop = await db.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
                if (shop == null)
                    throw new ArgumentException($"Shop with id {shopId} was not found in the database.");
                Lot lot = mapper.Map<LotDto, Lot>(lotData);
                lot.Shop = shop;

                await db.Lots.AddAsync(lot);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<LotDto>> GetCheapestLots(int amount)
        {
            throw new NotImplementedException();
            //using (var db = new BuckwheatContext())
            //{
            //    var lots = await db.Prices
            //        .OrderBy(x => x.Value)
            //        .Take(amount)
            //        .Select
            //        .ToListAsync();
            //}
        }

        public Task<List<LotDto>> GetCheapestLots(int shopId, int amount)
        {
            throw new NotImplementedException();
        }

        public Task<List<LotDto>> GetCheapestLots(int amount, DateTime afterDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<LotDto>> GetCheapestLots(int shopId, int amount, DateTime afterDate)
        {
            throw new NotImplementedException();
        }

        public Task<LotDto> GetLot(int lotId)
        {
            throw new NotImplementedException();
        }

        public Task<List<LotDto>> GetLots()
        {
            throw new NotImplementedException();
        }

        public Task<List<LotDto>> GetLots(int shopId)
        {
            throw new NotImplementedException();
        }
    }
}
