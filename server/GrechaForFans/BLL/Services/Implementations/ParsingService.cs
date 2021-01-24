using BLL.Parsers;
using BLL.Parsers.Implementations;
using DAL.Repositories;
using DataTransfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class ParsingService : IParsingService, IHostedService
    {
        IShopsRepository shopsRepository;
        ILotsService lotsService;
        IConfiguration config;

        List<IParser> parsers;
        CancellationTokenSource parsingCancelTokenSource = new CancellationTokenSource();

        public ParsingService(IShopsRepository shopsRepository,
            ILotsRepository lotsRepository,
            ILotsService lotsService,
            IConfiguration config)
        {
            this.shopsRepository = shopsRepository;
            this.lotsService = lotsService;
            this.config = config;
            parsers = new List<IParser>();
        }

        public async Task Initialize()
        {
            var keywords = config.GetSection("Parsing:Keywords").GetChildren().ToArray().Select(c => c.Value).ToArray();
            string pattern = keywords
                .Aggregate("", (acc, next) => acc + $"({next})|");
            pattern = pattern.Remove(pattern.Length - 1);
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            var promUaShop = await shopsRepository.GetShop("Prom.ua");
            var rozetkaShop = await shopsRepository.GetShop("Rozetka.com.ua");

            var promUaParser = new PromUaParser(config);
            await promUaParser.Initialize(promUaShop, regex);

            var rozetkaParser = new RozetkaParser(config);
            await rozetkaParser.Initialize(rozetkaShop, regex);

            parsers.Add(promUaParser);
            parsers.Add(rozetkaParser);
            await StartParsing();
        }

        public Task StartParsing()
        {
            Parse(parsingCancelTokenSource.Token);
            return Task.CompletedTask;
        }

        private async Task Parse(CancellationToken cancellationToken)
        {
            int pagesToParse = int.Parse(config["Parsing:PagesToParse"]);
            int parsingDelay = int.Parse(config["Parsing:ParsingDelay"]);

            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var parser in parsers)
                {
                    var parsedLots = await parser.ParseLots(pagesToParse);
                    await lotsService.UpdateLots(parsedLots);
                }
                await Task.Delay(parsingDelay * 1000);
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Initialize();
            await StartParsing();
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            parsingCancelTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
