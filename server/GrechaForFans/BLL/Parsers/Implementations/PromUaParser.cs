using AutoMapper.Configuration;
using DataTransfer;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Parsers.Implementations
{
    public class PromUaParser : IParser, IDisposable
    {
        private IWebDriver webDriver;
        private Microsoft.Extensions.Configuration.IConfiguration config;
        private ShopDto shop;
        private Regex keywordsRegex;
        private Regex kilogramsRegex = new Regex("\\d+\\s*(кг)", RegexOptions.IgnoreCase);
        private Regex gramsRegex = new Regex("\\d+\\s*(г)", RegexOptions.IgnoreCase);
        private bool disposedValue;

        public PromUaParser(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            this.config = config;
        }

        public async Task Initialize(ShopDto shop, Regex keywordsPattern)
        {
            await Task.Run(() =>
            {
                var options = new ChromeOptions();
                var path = Environment.GetEnvironmentVariable("CHROMEDRIVER_DIRECTORY");
                webDriver = new ChromeDriver(path, options);
                this.shop = shop;
                this.keywordsRegex = keywordsPattern;

            });
        }

        public async Task<List<LotDto>> ParseLots(int pagesAmount)
        {
            List<LotDto> result = new List<LotDto>();
            await Task.Run(async () =>
            {
                string url = config["Parsing:Addresses:Prom.ua"];

                for (int i = 0; i < pagesAmount; i++)
                {
                    string pageUrl = $"{url}&page={i + 1}";
                    webDriver.Navigate().GoToUrl(pageUrl);
                    await Task.Delay(500);
                    var lotDivs = webDriver.FindElements(By.CssSelector("div[data-company-id]"));
                    foreach (IWebElement lotDiv in lotDivs)
                    {
                        var lot = ParseLot(lotDiv);
                        if (lot != null)
                            result.Add(lot);
                    }
                }
            });
            return result;
        }

        private LotDto ParseLot(IWebElement lotDiv)
        {
            var a = lotDiv.FindElement(By.CssSelector("a[data-qaid=\"product_link\"]"));
            string title = a.GetAttribute("title");
            if (!keywordsRegex.IsMatch(title))
                return null;

            string link = new string(a.GetAttribute("href").TakeWhile(x => x != '?').ToArray());
            var imgElement = lotDiv.FindElement(By.CssSelector("img"));
            string imgLink = imgElement.GetAttribute("src");
            string priceStr = lotDiv.FindElement(By.CssSelector("span[data-qaprice]")).GetAttribute("data-qaprice");
            decimal price = decimal.Parse(new string(priceStr.TakeWhile(x => Char.IsDigit(x)).ToArray()));
            int grams = GetGrams(title);
            
            return new LotDto()
            {
                Shop = this.shop,
                ImageLink = imgLink,
                Link = link,
                Manufacturer = null,
                Title = title,
                WeightInGrams = grams,
                Price = new PriceDto()
                {
                    Date = DateTime.Now,
                    Value = price
                }
            };
        }

        private int GetGrams(string str)
        {
            var gramsRes = gramsRegex.Match(str);
            if (gramsRes.Success)
                return int.Parse(new string(gramsRes.Value.TakeWhile(x => char.IsDigit(x)).ToArray()));
            var kilogramsRes = kilogramsRegex.Match(str);
            if (kilogramsRes.Success)
                return int.Parse(new string(kilogramsRes.Value.TakeWhile(x => char.IsDigit(x)).ToArray())) * 1000;
            return 1000;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    webDriver.Quit();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
