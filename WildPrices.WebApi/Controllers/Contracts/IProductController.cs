using Microsoft.AspNetCore.Mvc;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Data.Entities;

namespace WildPrices.WebApi.Controllers.Contracts
{
    public interface IProductController
    {
        Task<ActionResult<IEnumerable<ProductCardForViewDto>>> GetAllProductAsync();

        Task<ActionResult<IEnumerable<ProductCardForViewDto>>> GetAllIsDesiredProductsAsync();

        Task<ActionResult<IEnumerable<ProductCardForViewDto>>> GetAllIsNotDesiredProductsAsync();

        Task<IActionResult> CreateProductAsync(ProductRequestDto product);

        Task<IActionResult> UpdateMinAndMaxPriceAsync(string article);

        Task<IActionResult> UpdateDesiredPriceAsync(string article, DesiredPriceDto desiredPriceDto);

        Task<IActionResult> DeleteProductAsync(string article);

        Task<ActionResult<CountProductsDto>> CountProducts();
    }
}
