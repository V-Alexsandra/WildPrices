using AngleSharp;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.Services.Common;

namespace WildPrices.Business.Services.Implementation
{
    public class ParserService : IParserService
    {
        public async Task<ProductFromWildberriesDto> GetProductByArticleAsync(string article)
        {
            string url = $"https://www.wildberries.by/product?card={article}";

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            var document = await context.OpenAsync(url);

            string brandName = document.QuerySelector("span.product__brand-name").TextContent.Trim();
            string productName = document.QuerySelector("span[data-tag=\"productName\"]").TextContent.Trim();

            string name = brandName + " / " + productName;

            string image = document.QuerySelector("picture img.swiper-slide__img").GetAttribute("src");
            // Создаем объект

            string currentPrice = document.QuerySelector("div.product-price-current span.product-price-current__value").TextContent.Trim();

            currentPrice = currentPrice.Replace(",", ".");

            var productFromWildberries = new ProductFromWildberriesDto
            {
                Name = name,
                Link = url,
                Image = image,
                Article = Convert.ToInt32(article),
                CurrentPrice = Convert.ToDouble(currentPrice),
                CurrentDate = DateTime.Now.ToString()
            };

            return productFromWildberries;
        }
    }
}
