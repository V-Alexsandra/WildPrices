using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Text;
using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.Services.Common;
using static System.Net.WebRequestMethods;

namespace WildPrices.Business.Services.Implementation
{
    public class ParserService : IParserService
    {
        public ProductFromWildberriesDto GetProductByArticle(string article)
        {
            string url = $"https://card.wb.ru/cards/detail?nm={article};{article};{article}&appType=128&curr=byn&locale=by&lang=ru&dest=-59208&regions=1,4,22,30,31,33,40,48,66,68,69,70,80,83,111,114,115&reg=1&spp=0";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string json = reader.ReadToEnd();

                        dynamic jsonObj = JsonConvert.DeserializeObject(json);

                        string productName = jsonObj.data.products[0].name;
                        string brand = jsonObj.data.products[0].brand;

                        string name = brand + " / " + productName;

                        var productFromWildberries = new ProductFromWildberriesDto
                        {
                            Name = name,
                            Link = $"https://www.wildberries.by/product?card={article}",
                            Image = "image",
                            Article = Convert.ToInt32(article),
                        };

                        return productFromWildberries;
                    }
                }
            }
        }

        public PriceHistoryForCreationDto GetPriceHistory(ProductFromWildberriesDto productFromWildberries)
        {
            string article = productFromWildberries.Article.ToString();
            
            string url = $"https://card.wb.ru/cards/detail?nm={article};{article};{article}&appType=128&curr=byn&locale=by&lang=ru&dest=-59208&regions=1,4,22,30,31,33,40,48,66,68,69,70,80,83,111,114,115&reg=1&spp=0";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string json = reader.ReadToEnd();

                        dynamic jsonObj = JsonConvert.DeserializeObject(json);

                        string salePriceU = jsonObj.data.products[0].salePriceU;

                        string currentPrice = salePriceU.Substring(0, salePriceU.Length - 2) + "." + salePriceU.Substring(salePriceU.Length - 2);

                        string currentDate = DateTime.Now.ToShortDateString();
                        DateTime date = DateTime.ParseExact(currentDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        string newDateStr = date.ToString("dd/MM/yyyy");

                        var priceHistoryForCreationDto = new PriceHistoryForCreationDto
                        {
                            CurrentPrice = Convert.ToDouble(currentPrice),
                            CurrentDate = newDateStr
                        };

                        return priceHistoryForCreationDto;
                    }
                }
            }
        }
    }
}
