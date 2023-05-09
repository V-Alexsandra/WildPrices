using Microsoft.AspNetCore.Mvc;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.Services.Common;
using WildPrices.WebApi.Controllers.Contracts;

namespace WildPrices.WebApi
{
    public class DailyJob : BackgroundService
    {
        private readonly IPriceHistoryController _priceHistoryController;
        private readonly IProductController _productController;
        private readonly IProductService _productService;

        public DailyJob(IPriceHistoryController priceHistoryController, IProductController productController,
            IProductService productService)
        {
            _priceHistoryController = priceHistoryController;
            _productController = productController;
            _productService = productService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _productController.GetAllProductAsync();

                if (result.Result is OkObjectResult okObjectResult)
                {
                    var products = okObjectResult.Value as IEnumerable<ProductCardForViewDto>;

                    foreach (var product in products)
                    {
                        await _priceHistoryController.CreatePriceHistory(product.Article.ToString());
                        await _productController.UpdateMinAndMaxPriceAsync(await _productService.GetProductIdByArticleAsync(product.Article));
                    }
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
