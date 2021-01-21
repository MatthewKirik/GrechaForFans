
using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ILotsRepository
    {
        Task AddLot(LotDto lotData, int shopId);
        Task<LotDto> GetLot(int lotId);
        Task<List<LotDto>> GetLots();
        Task<List<LotDto>> GetLots(int shopId);
        Task<List<LotDto>> GetCheapestLots(int amount);
        Task<List<LotDto>> GetCheapestLots(int shopId, int amount);
        Task<List<LotDto>> GetCheapestLots(int amount, DateTime afterDate);
        Task<List<LotDto>> GetCheapestLots(int shopId, int amount, DateTime afterDate);
    }
}
