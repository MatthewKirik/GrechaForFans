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
    public class PricesRepository : IPricesRepository
    {
        IMapper mapper;
        public PricesRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public async Task AddPrice(PriceDto priceData, int lotId)
        {
            using (var db = new BuckwheatContext())
            {
                Lot lot = await db.Lots.FirstOrDefaultAsync(x => x.Id == lotId);
                if (lot == null)
                    throw new ArgumentException($"Lot with id {lotId} was not found in the database.");
                Price price = mapper.Map<PriceDto, Price>(priceData);
                price.Lot = lot;
                
                await db.Prices.AddAsync(price);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<PriceDto>> GetPrices(int lotId)
        {
            using (var db = new BuckwheatContext())
            {
                var prices = await db.Prices
                    .Where(x => x.Lot.Id == lotId)
                    .ToListAsync();
                return mapper.Map<List<Price>, List<PriceDto>>(prices);
            }
        }
    }
}
