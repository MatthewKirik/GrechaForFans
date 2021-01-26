using BLL.Parsers;
using BLL.Parsers.Implementations;
using DAL.Repositories;
using DataTransfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium.Chrome;
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

            var options = new ChromeOptions();
            options.AddArguments("headless");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("no-sandbox");
            var path = Environment.GetEnvironmentVariable("CHROMEDRIVER_DIRECTORY");
            ChromeDriverService chromeService;
            if (path != null)
                chromeService = ChromeDriverService.CreateDefaultService(path);
            else
                chromeService = ChromeDriverService.CreateDefaultService();
            var webDriver = new ChromeDriver(chromeService, options, TimeSpan.FromMinutes(3));
            webDriver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(30));

            var promUaShop = await shopsRepository.GetShop("Prom");
            var rozetkaShop = await shopsRepository.GetShop("Rozetka");
            var epicentrShop = await shopsRepository.GetShop("Epicentr");

            var promUaParser = new PromUaParser(config, promUaShop, regex, webDriver);
            var rozetkaParser = new RozetkaParser(config, rozetkaShop, regex, webDriver);
            var epicentrParser = new EpicentrParser(config, epicentrShop, regex, webDriver);

            parsers.Add(promUaParser);
            parsers.Add(rozetkaParser);
            parsers.Add(epicentrParser);
        }

        public Task StartParsing()
        {
            Parse(parsingCancelTokenSource.Token);
            return Task.CompletedTask;
        }

        private async void Parse(CancellationToken cancellationToken)
        {
            Console.WriteLine("Parsing started");
            int pagesToParse = int.Parse(config["Parsing:PagesToParse"]);
            int parsingDelay = int.Parse(config["Parsing:ParsingDelay"]);

            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var parser in parsers)
                {
                    await ParseAndUpdate(parser, pagesToParse);
                }
                await Task.Delay(parsingDelay * 1000);
            }
        }

        private async Task ParseAndUpdate(IParser parser, int pagesToParse)
        {
            var parsedLots = await parser.ParseLots(pagesToParse);
            await lotsService.UpdateLots(parsedLots);
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
