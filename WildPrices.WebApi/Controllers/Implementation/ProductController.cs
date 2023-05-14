using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.Exceptions;
using WildPrices.Business.Services.Common;
using WildPrices.Data.Entities;
using WildPrices.WebApi.Controllers.Contracts;

namespace WildPrices.WebApi.Controllers.Implementation
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase, IProductController
    {
        private readonly IProductService _productService;
        private readonly IParserService _parserService;
        private readonly IMemoryCache _memoryCache;
        private readonly IPriceHistoryService _priceHistoryService;

        public ProductController(IProductService productService, IParserService parserService,
            IMemoryCache memoryCache, IPriceHistoryService priceHistoryService)
        {
            _productService = productService;
            _parserService = parserService;
            _memoryCache = memoryCache;
            _priceHistoryService = priceHistoryService;
        }

        [HttpGet]
        [Route("getAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductCardForViewDto>>> GetAllProductAsync()
        {
            if (_memoryCache.TryGetValue("UserId", out string? id))
            {
                if (id == null)
                {
                    throw new NotSucceededException(nameof(id));
                }

                var products = await _productService.GetAllProductAsync(id);

                return Ok(products);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("getAllIsDesiredProducts")]
        public async Task<ActionResult<IEnumerable<ProductCardForViewDto>>> GetAllIsDesiredProductsAsync()
        {
            if (_memoryCache.TryGetValue("UserId", out string? id))
            {
                if (id == null)
                {
                    throw new NotSucceededException(nameof(id));
                }

                var products = await _productService.GetAllIsDesiredProductsAsync(id);

                return Ok(products);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("getAllIsNotDesiredProducts")]
        public async Task<ActionResult<IEnumerable<ProductCardForViewDto>>> GetAllIsNotDesiredProductsAsync()
        {
            if (_memoryCache.TryGetValue("UserId", out string? id))
            {
                if (id == null)
                {
                    throw new NotSucceededException(nameof(id));
                }

                var notDesiredProducts = await _productService.GetAllIsNotDesiredProductsAsync(id);
                return Ok(notDesiredProducts);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("createProduct")]
        public async Task<IActionResult> CreateProductAsync(ProductRequestDto product)
        {
            if(product == null)
            {

                throw new ArgumentNullException(nameof(product));
            }

            var desiredPriceDto = _productService.GetDisiredPrice(product.DesiredPrice);
            var model = _parserService.GetProductByArticle(product.Article);

            if (_memoryCache.TryGetValue("UserId", out string? id))
            {
                if (id == null)
                {
                    throw new NotSucceededException(nameof(id));
                }

                await _productService.CreateProductWhenDoesNotExistAsync(model, desiredPriceDto, id);
            }

            return Ok();
        }

        [HttpPut("{id}/updateMinAndMaxPrice")]
        public async Task<IActionResult> UpdateMinAndMaxPriceAsync(string article)
        {
            var id = await _productService.GetProductIdByArticleAsync(Convert.ToInt32(article));

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
            return Ok();
        }

        [HttpPut("{id}/updateDesiredPrice")]
        public async Task<IActionResult> UpdateDesiredPriceAsync(int id, DesiredPriceDto desiredPriceDto)
        {
            var product = await _productService.GetProductByIdAsync(id);

            var currentPrice = await _priceHistoryService.GetCurrentPriceAsync(id);

            if (currentPrice == desiredPriceDto.DesiredPrice)
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
                MaxPrice = product.MaxPrice,
                MinPrice = product.MinPrice,
                DesiredPrice = desiredPriceDto.DesiredPrice,
                IsDesiredPrice = product.IsDesiredPrice,
                UserId = product.UserId
            };

            await _productService.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete("{article}")]
        public async Task<IActionResult> DeleteProductAsync(string article)
        {
            await _productService.DeleteProductAsync(await _productService.GetProductIdByArticleAsync(Convert.ToInt32(article)));
            await _priceHistoryService.DeleteAllByProductIdAsync(await _productService.GetProductIdByArticleAsync(Convert.ToInt32(article)));

            return Ok();
        }

        [HttpGet]
        [Route("countProducts")]
        public async Task<ActionResult<CountProductsDto>> CountProducts()
        {
            if (_memoryCache.TryGetValue("UserId", out string? id))
            {
                if (id == null)
                {
                    throw new NotSucceededException(nameof(id));
                }

                var countProductsDto = await _productService.GetProductsCountAsync(id);

                return Ok(countProductsDto);
            }

            return NotFound();
        }
    }
}