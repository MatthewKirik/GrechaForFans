
using DataTransfer;
using DataTransfer.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ILotsRepository
    {
        Task<LotDto> AddLot(LotDto lotData, int shopId);
        Task<LotDto> GetLot(int lotId);
        Task<int?> GetLotId(string link);
        Task<List<LotDto>> GetLots(LotFilter filter);
        Task<List<LotDto>> GetCheapestLots(LotFilter filter, DateTime? toDate = null);
    }
}
