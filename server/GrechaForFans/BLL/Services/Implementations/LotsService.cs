using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class LotsService : ILotsService
    {
        public Task<List<LotDto>> GetCheapestLots(int amount)
        {
            throw new NotImplementedException();
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
