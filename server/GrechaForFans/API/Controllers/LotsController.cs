using BLL.Services;
using DataTransfer;
using DataTransfer.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotsController : ControllerBase
    {
        ILotsService lotsService;

        public LotsController(ILotsService lotsService)
        {
            this.lotsService = lotsService;
        }

        [HttpGet("{lotId}")]
        public async Task<LotDto> GetLot(int lotId)
            => await lotsService.GetLot(lotId);

        [HttpGet]
        public async Task<List<LotDto>> GetLots([FromQuery] LotFilter filter)
            => await lotsService.GetLots(filter);
    }
}
