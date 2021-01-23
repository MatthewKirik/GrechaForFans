using BLL.Parsers;
using BLL.Parsers.Implementations;
using DAL.Repositories;
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
        IConfiguration config;

        List<IParser> parsers;
        CancellationTokenSource parsingCancelTokenSource = new CancellationTokenSource();

        public ParsingService(IShopsRepository shopsRepository,
            IConfiguration config)
        {
            this.shopsRepository = shopsRepository;
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
            var promUaParser = new PromUaParser(config);
            await promUaParser.Initialize(promUaShop, regex);

            parsers.Add(promUaParser);
        }

        public Task StartParsing()
        {
            Parse(parsingCancelTokenSource.Token);
            return Task.CompletedTask;
        }

        private async Task Parse(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var parsed = await parsers[0].ParseLots(2);
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
