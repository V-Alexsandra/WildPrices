using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.Services.Common
{
    public interface IProductService
    {
        DesiredPriceDto GetDisiredPriceAsync(double desiredPrice);

        Task CreateProductWhenDoesNotExistAsync(ProductFromWildberriesDto model, DesiredPriceDto desiredPriceDto, MinAndMaxPriceDto minAndMaxPriceDto);

        Task<IEnumerable<ProductEntity>> GetAllProductAsync();

        Task<IEnumerable<ProductEntity>> GetAllIsDesiredProductsAsync();

        Task<IEnumerable<ProductEntity>> GetAllIsNotDesiredProductsAsync();

        Task<int> GetProductArticleByIdAsync(int id);

        Task UpdateAsync(ProductForUpdateDto model);

        Task DeleteProductAsync(int id);
    }
}
