using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        IPricesService pricesService;

        public PricesController(IPricesService pricesService)
        {
            this.pricesService = pricesService;
        }

        [HttpGet("{lotId}")]
        public async Task<IEnumerable<PriceDto>> GetPrices(int lotId)
            => await pricesService.GetPrices(lotId);
    }
}
