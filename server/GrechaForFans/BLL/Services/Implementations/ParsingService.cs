using BLL.Parsers;
using BLL.Parsers.Implementations;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class ParsingService : IParsingService
    {
        IShopsRepository shopsRepository;

        List<IParser> parsers;

        public ParsingService(IShopsRepository shopsRepository)
        {
            this.shopsRepository = shopsRepository;
            parsers = new List<IParser>();
        }

        public async Task Initialize()
        {
            var promUaShop = await shopsRepository.GetShop("Prom.ua");
            var promUaParser = new PromUaParser();
            await promUaParser.Initialize(promUaShop);

            parsers.Add(promUaParser);
        }

        public Task StartParsing()
        {
            throw new NotImplementedException();
        }
    }
}
