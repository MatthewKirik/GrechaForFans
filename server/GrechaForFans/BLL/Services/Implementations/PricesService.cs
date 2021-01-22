using DAL.Repositories;
using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class PricesService : IPricesService
    {
        IPricesRepository pricesRepository;

        public PricesService(IPricesRepository pricesRepository)
        {
            this.pricesRepository = pricesRepository;
        }

        public Task<List<PriceDto>> GetPrices(int lotId)
            => pricesRepository.GetPrices(lotId);
    }
}
