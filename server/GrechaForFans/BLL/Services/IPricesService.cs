using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IPricesService
    {
        Task<List<PriceDto>> GetPrices(int lotId);
    }
}
