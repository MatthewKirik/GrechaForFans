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
            var fromDate = filter.FromDate ?? DateTime.MinValue;
            var toDate = filter.ToDate ?? DateTime.MaxValue;
            var limit = (filter.Limit > 0 & filter.Limit <= 50) ? filter.Limit : 50;

            using (var db = new BuckwheatContext())
            {
                var lotsQuery = db.Lots.AsQueryable();
                if (filter.ShopId != null)
                    lotsQuery = lotsQuery.Where(x => x.Shop.Id == filter.ShopId);
                if (filter.FromWeight != null)
                    lotsQuery = lotsQuery.Where(x => x.WeightInGrams >= filter.FromWeight);
                if (filter.ToWeight != null)
                    lotsQuery = lotsQuery.Where(x => x.WeightInGrams <= filter.ToWeight);

                var lastPricesQuery = lotsQuery.Select(lot =>
                        db.Prices
                        .Where(p => p.Lot.Id == lot.Id && p.Date >= fromDate && p.Date <= toDate)
                        .OrderByDescending(p => p.Date)
                        .FirstOrDefault());

                var orderedPricesQuery = lastPricesQuery;
                if (filter.Order == "expensive")
                    orderedPricesQuery = orderedPricesQuery.OrderByDescending(x => x.Value);
                else if(filter.Order == "cheap")
                    orderedPricesQuery = orderedPricesQuery.OrderBy(x => x.Value);

                var oredredLotsQuery = orderedPricesQuery
                    .Take(limit)
                    .Select(x => new { Lot = x.Lot, Price = x });

                var orderedLots = await oredredLotsQuery.ToListAsync();
                var orderedLotsDtos = orderedLots.Select(x =>
                {
                    var mapped = mapper.Map<Lot, LotDto>(x.Lot);
                    mapped.Price = mapper.Map<Price, PriceDto>(x.Price);
                    return mapped;
                });

                return orderedLotsDtos.ToList();
            }
        }
    }
}
