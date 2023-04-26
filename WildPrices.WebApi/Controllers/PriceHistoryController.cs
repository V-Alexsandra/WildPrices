using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Business.Services.Common;

namespace WildPrices.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PriceHistoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPriceHistoryService _priceHistoryService;
        private readonly IParserService _parserService;
        private readonly IProductService _productService;

        public PriceHistoryController(IMapper mapper, IPriceHistoryService priceHistoryService,
            IParserService parserService, IProductService productService)
        {
            _mapper = mapper;
            _priceHistoryService = priceHistoryService;
            _parserService = parserService;
            _productService = productService;
        }

        [HttpPost("{article}")]
        public async Task<IActionResult> CreatePriceHistory(string article)
        {
            var productFromWildberries = _parserService.GetProductByArticle(article);

            var priceHistoryForCreation = _parserService.GetPriceHistory(productFromWildberries);

            priceHistoryForCreation.ProductId = await _productService.GetProductIdByArticleAsync(productFromWildberries.Article);

            await _priceHistoryService.CreatePriceHistoryAsync(priceHistoryForCreation);
            return Ok();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAllPriceHistoryByProductId(int productId)
        {
            var priceHistoryEntities = await _priceHistoryService.GetAllPriceHistoryByProductIdAsync(productId);
            var priceHistoryDtos = _mapper.Map<IEnumerable<PriceHistoryDto>>(priceHistoryEntities);

            return Ok(priceHistoryDtos);
        }

        //testing call in product controller please

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAllByProductId(int productId)
        {
            await _priceHistoryService.DeleteAllByProductIdAsync(productId);
            return Ok();
        }

        [HttpGet("{productId}/min-and-max-price")]
        public async Task<IActionResult> GetMaxAndMinPrice(int productId)
        {
            var minAndMaxPriceDto = await _priceHistoryService.GetMaxAndMinPriceAsync(productId);
            return Ok(minAndMaxPriceDto);
        }
    }
}
