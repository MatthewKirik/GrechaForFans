using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
    public class PricesRepository : IPricesRepository
    {
        public Task AddPrice(PriceDto priceData, int lotId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PriceDto>> GetPrices(int lotId)
        {
            throw new NotImplementedException();
        }
    }
}
