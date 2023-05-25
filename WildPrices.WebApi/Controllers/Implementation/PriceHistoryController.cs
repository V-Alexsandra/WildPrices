﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Business.Services.Common;
using WildPrices.WebApi.Controllers.Contracts;

namespace WildPrices.WebApi.Controllers.Implementation
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PriceHistoryController : ControllerBase, IPriceHistoryController
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

        [HttpGet]
        public async Task<IActionResult> GetAllPriceHistoryByProductId(string article)
        {
            var productId = await _productService.GetProductIdByArticleAsync(Convert.ToInt32(article));

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

        [HttpGet("/min-and-max-price")]
        public async Task<IActionResult> GetMaxAndMinPrice(string article)
        {
            var id = await _productService.GetProductIdByArticleAsync(Convert.ToInt32(article));

            var minAndMaxPriceDto = await _priceHistoryService.GetMaxAndMinPriceAsync(id);
            return Ok(minAndMaxPriceDto);
        }
    }
}
