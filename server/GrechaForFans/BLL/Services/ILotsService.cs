using DataTransfer;
using DataTransfer.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ILotsService
    {
        Task<LotDto> GetLot(int lotId);
        Task<List<LotDto>> GetLots(LotFilter filter);
        Task UpdateLots(List<LotDto> newLots);
    }
}
