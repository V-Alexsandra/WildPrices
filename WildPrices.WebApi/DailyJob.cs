using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.Exceptions;
using WildPrices.Business.Services.Common;
using WildPrices.Business.Services.Implementation;

namespace WildPrices.WebApi
{
    public class DailyJob : BackgroundService
    {
        private  IMemoryCache _memoryCache;
        private  IProductService _productService;
        private  IPriceHistoryService _priceHistoryService;
        private  IParserService _parserService;
        private  IServiceScopeFactory _serviceScopeFactory;

        public DailyJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();  
                _priceHistoryService = scope.ServiceProvider.GetRequiredService<IPriceHistoryService>();
                _productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                _parserService = scope.ServiceProvider.GetRequiredService<IParserService>();
                _memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

                var products = await GetAllProducts();

                foreach (var product in products)
                {
                    await CreatePriceHistory(product.Article.ToString());
                    await Update(await _productService.GetProductIdByArticleAsync(product.Article));
                }

            }
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }

        public async Task<IEnumerable<ProductCardForViewDto>> GetAllProducts()
        {
            var products = await _productService.GetAllProductWithoutUserIdAsync();

            return products;
        }

        public async Task CreatePriceHistory(string article)
        {
            var productFromWildberries = _parserService.GetProductByArticle(article);

            var priceHistoryForCreation = _parserService.GetPriceHistory(productFromWildberries);

            priceHistoryForCreation.ProductId = await _productService.GetProductIdByArticleAsync(productFromWildberries.Article);

            await _priceHistoryService.CreatePriceHistoryAsync(priceHistoryForCreation);
        }

        public async Task Update(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            var minAndMaxPrice = await _priceHistoryService.GetMaxAndMinPriceAsync(id);

            var currentPrice = await _priceHistoryService.GetCurrentPriceAsync(id);

            if (currentPrice >= product.DesiredPrice - 1 && currentPrice <= product.DesiredPrice + 1)
            {
                product.IsDesiredPrice = true;
            }

            var model = new ProductForUpdateDto
            {
                Id = id,
                Article = product.Article,
                Name = product.Name,
                Link = product.Link,
                Image = product.Image,
                MaxPrice = minAndMaxPrice.MaxPrice,
                MinPrice = minAndMaxPrice.MinPrice,
                DesiredPrice = product.DesiredPrice,
                IsDesiredPrice = product.IsDesiredPrice,
                UserId = product.UserId
            };

            await _productService.UpdateAsync(model);
        }
    }
}
