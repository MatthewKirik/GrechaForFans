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
    public class ShopsController : ControllerBase
    {
        IShopsService shopsService;

        public ShopsController(IShopsService shopsService)
        {
            this.shopsService = shopsService;
        }

        [HttpGet]
        public async Task<IEnumerable<ShopDto>> GetShops()
            => await shopsService.GetShops();

        [HttpGet("{id}")]
        public async Task<ShopDto> GetShop(int id)
            => await shopsService.GetShop(id);
    }
}
