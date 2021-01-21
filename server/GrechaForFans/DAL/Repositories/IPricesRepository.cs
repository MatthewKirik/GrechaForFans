using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IPricesRepository
    {
        Task AddPrice(PriceDto priceData, int lotId);
        Task<List<PriceDto>> GetPrices(int lotId);
    }
}
