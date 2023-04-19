using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.Exceptions;
using WildPrices.Business.Services.Common;
using WildPrices.Data.Entities;

namespace WildPrices.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
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
        public async Task<ActionResult<IEnumerable<ProductEntity>>> GetAllProductAsync()
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
        public async Task<ActionResult<IEnumerable<ProductEntity>>> GetAllIsDesiredProductsAsync()
        {
            var products = await _productService.GetAllIsDesiredProductsAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("getAllIsNotDesiredProducts")]
        public async Task<ActionResult<IEnumerable<ProductEntity>>> GetAllIsNotDesiredProductsAsync()
        {
            var notDesiredProducts = await _productService.GetAllIsNotDesiredProductsAsync();
            return Ok(notDesiredProducts);
        }

        [HttpPost]
        [Route("createProduct")]
        public async Task<IActionResult> CreateProductAsync(double desiredPrice, string article)
        {
            var desiredPriceDto = _productService.GetDisiredPrice(desiredPrice);
            var model = _parserService.GetProductByArticle(article);

            if (_memoryCache.TryGetValue("UserId", out string? id))
            {
                if(id == null)
                {
                    throw new NotSucceededException(nameof(id));
                }

                await _productService.CreateProductWhenDoesNotExistAsync(model, desiredPriceDto, id);
            }

            return Ok();
        }

        [HttpPut("{id}/updateMinAndMaxPrice")]
        public async Task<IActionResult> UpdateMinAndMaxPriceAsync(int id)
        {
            var product = await _productService.GetProductById(id);

            var minAndMaxPrice = await _priceHistoryService.GetMaxAndMinPriceAsync(id);

            var currentPrice = await _priceHistoryService.GetCurrentPriceAsync(id);

            if(currentPrice == product.DesiredPrice)
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
        public async Task<IActionResult> UpdateDesiredPriceAsync(int id,  DesiredPriceDto desiredPriceDto)
        {
            var product = await _productService.GetProductById(id);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            await _productService.DeleteProductAsync(id);
            await _priceHistoryService.DeleteAllByProductIdAsync(id);

            return Ok();
        }
    }
}