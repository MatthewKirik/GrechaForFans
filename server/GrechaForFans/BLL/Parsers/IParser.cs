using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Parsers
{
    interface IParser
    {
        public Task<List<LotDto>> ParseLots(int pagesAmount);
    }
}
