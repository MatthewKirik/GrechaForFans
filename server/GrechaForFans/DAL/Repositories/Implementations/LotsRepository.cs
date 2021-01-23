using AutoMapper;
using DAL.Models;
using DataTransfer;
using DataTransfer.Filters;
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

        public async Task<LotDto> AddLot(LotDto lotData, int shopId)
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

                return mapper.Map<Lot, LotDto>(lot);
            }
        }

        public async Task<List<LotDto>> GetCheapestLots(LotFilter filter, DateTime? toDate = null)
        {
            toDate ??= DateTime.MaxValue;
            var limit = (filter.Limit > 0 & filter.Limit <= 50) ? filter.Limit : 50;

            using (var db = new BuckwheatContext())
            {
                var lotsQuery = db.Lots.AsQueryable();
                if (filter.ShopId != null)
                    lotsQuery = lotsQuery.Where(x => x.Shop.Id == filter.ShopId);
                if (filter.WeightInGrams != null)
                    lotsQuery = lotsQuery.Where(x => x.WeightInGrams == filter.WeightInGrams);

                var lastPricesQuery = lotsQuery.Select(x =>
                        db.Prices
                        .Where(x => x.Lot.Id == x.Id && x.Date <= toDate)
                        .OrderByDescending(x => x.Date)
                        .FirstOrDefault());

                var cheapestLotsQuery = lastPricesQuery
                    .OrderBy(x => x.Value)
                    .Take(filter.Limit)
                    .Select(x => x.Lot);

                return mapper.Map<List<Lot>, List<LotDto>>(await cheapestLotsQuery.ToListAsync());
            }
        }

        public async Task<LotDto> GetLot(int lotId)
        {
            using (var db = new BuckwheatContext())
            {
                var lot = await db.Lots.FirstOrDefaultAsync(x => x.Id == lotId);
                return mapper.Map<Lot, LotDto>(lot);
            }
        }

        public async Task<int?> GetLotId(string link)
        {
            using (var db = new BuckwheatContext())
            {
                var lot = await db.Lots.FirstOrDefaultAsync(x => x.Link == link);
                return lot?.Id;
            }
        }

        public async Task<List<LotDto>> GetLots(LotFilter filter)
        {
            var limit = (filter.Limit > 0 & filter.Limit <= 50) ? filter.Limit : 50;
            using (var db = new BuckwheatContext())
            {
                var lots = db.Lots.AsQueryable();
                if (filter.ShopId != null) 
                    lots = lots.Where(x => x.Shop.Id == filter.ShopId);
                if (filter.WeightInGrams != null)
                    lots = lots.Where(x => x.WeightInGrams == filter.WeightInGrams);
                lots = lots.Take(limit);
                return mapper.Map<List<Lot>, List<LotDto>>(await lots.ToListAsync());
            }
        }
    }
}
