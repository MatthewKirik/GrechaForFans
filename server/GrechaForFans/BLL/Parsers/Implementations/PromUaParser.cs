using DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Parsers.Implementations
{
    public class PromUaParser : IParser
    {
        AngleSharp.IBrowsingContext browsingContext;
        public PromUaParser()
        {
            var config = AngleSharp.Configuration.Default;
            this.browsingContext = AngleSharp.BrowsingContext.New(config);
        }

        public Task<List<LotDto>> ParseLots(int pagesAmount)
        {
            throw new NotImplementedException();
        }
    }
}
