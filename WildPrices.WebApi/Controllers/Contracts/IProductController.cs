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

        Task<IActionResult> CreateProductAsync(double desiredPrice, string article);

        Task<IActionResult> UpdateMinAndMaxPriceAsync(int id);

        Task<IActionResult> UpdateDesiredPriceAsync(int id, DesiredPriceDto desiredPriceDto);

        Task<IActionResult> DeleteProductAsync(int id);

        Task<ActionResult<CountProductsDto>> CountProducts();
    }
}
