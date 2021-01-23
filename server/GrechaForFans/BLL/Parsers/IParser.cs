using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Parsers
{
    interface IParser
    {
        public Task Initialize(ShopDto shop, Regex keywordsPattern);
        public Task<List<LotDto>> ParseLots(int pagesAmount);
    }
}
