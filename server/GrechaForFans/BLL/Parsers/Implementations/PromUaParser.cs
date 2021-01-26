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
        private Regex floatKilogramsRegex = new Regex("(0,)\\d+\\s*(кг)", RegexOptions.IgnoreCase);
        private Regex kilogramsRegex = new Regex("\\d+\\s*(кг)", RegexOptions.IgnoreCase);
        private Regex gramsRegex = new Regex("\\d+\\s*(г)", RegexOptions.IgnoreCase);
        private bool disposedValue;

        public PromUaParser(
            Microsoft.Extensions.Configuration.IConfiguration config,
            ShopDto shop,
            Regex keywordsPattern,
            IWebDriver webDriver)
        {
            this.config = config;
            this.webDriver = webDriver;
            this.shop = shop;
            this.keywordsRegex = keywordsPattern;
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
            try
            {
                var a = lotDiv.FindElement(By.CssSelector("a[data-qaid=\"product_link\"]"));
                string title = a.GetAttribute("title");
                if (!keywordsRegex.IsMatch(title))
                    return null;

                string link = new string(a.GetAttribute("href").TakeWhile(x => x != '?').ToArray());
                var imgElement = lotDiv.FindElement(By.CssSelector("img"));
                string imgLink = imgElement.GetAttribute("src");
                string priceStr = lotDiv.FindElement(By.CssSelector("span[data-qaprice]")).GetAttribute("data-qaprice");
                priceStr = new string(priceStr.Where(x => x != ' ').ToArray());
                decimal price = decimal.Parse(new string(priceStr.TakeWhile(x => Char.IsDigit(x)).ToArray()));
                int grams = ParsingUtils.GetGrams(title);

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
            catch (Exception ex)
            {
                return null;
            }
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
