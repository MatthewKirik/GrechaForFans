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

        public LotsService(ILotsRepository lotsRepository)
        {
            this.lotsRepository = lotsRepository;
        }

        public Task<List<LotDto>> GetCheapestLots(LotFilter filter, DateTime? toDate = null)
            => lotsRepository.GetCheapestLots(filter, toDate);

        public Task<LotDto> GetLot(int lotId)
            => lotsRepository.GetLot(lotId);

        public Task<List<LotDto>> GetLots(LotFilter filter)
            => lotsRepository.GetLots(filter);
    }
}
