using DAL.Repositories;
using DataTransfer;
using DataTransfer.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class LotsService : ILotsService
    {
        ILotsRepository lotsRepository;
        IPricesRepository pricesRepository;

        public LotsService(ILotsRepository lotsRepository,
            IPricesRepository pricesRepository)
        {
            this.lotsRepository = lotsRepository;
            this.pricesRepository = pricesRepository;
        }

        public async Task UpdateLots(List<LotDto> newLots)
        {
            foreach (var newLot in newLots)
            {
                int? oldLotId = await lotsRepository.GetLotId(newLot.Link);
                if(oldLotId != null)
                {
                    await pricesRepository.AddPrice(newLot.Price, (int)oldLotId);
                }
                else
                {
                    var addedLot = await lotsRepository.AddLot(newLot, newLot.Shop.Id);
                    await pricesRepository.AddPrice(newLot.Price, addedLot.Id);
                }
            }

        }

        public Task<List<LotDto>> GetCheapestLots(LotFilter filter, DateTime? toDate = null)
            => lotsRepository.GetCheapestLots(filter, toDate);

        public Task<LotDto> GetLot(int lotId)
            => lotsRepository.GetLot(lotId);

        public Task<List<LotDto>> GetLots(LotFilter filter)
            => lotsRepository.GetLots(filter);
    }
}
