using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        [HttpGet("lowest")]
        public async Task<IEnumerable<string>> GetLowestPrices()
        {
            return new string[] { "hi", "there" };
        }
    }
}
