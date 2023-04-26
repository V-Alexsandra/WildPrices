using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.Services.Common
{
    public interface IProductService
    {
        DesiredPriceDto GetDisiredPrice(double desiredPrice);

        Task CreateProductWhenDoesNotExistAsync(ProductFromWildberriesDto model, DesiredPriceDto desiredPriceDto, string id);

        Task<IEnumerable<ProductCardForViewDto>> GetAllProductAsync(string userId);

        Task<IEnumerable<ProductCardForViewDto>> GetAllIsDesiredProductsAsync(string userId);

        Task<IEnumerable<ProductCardForViewDto>> GetAllIsNotDesiredProductsAsync(string userId);

        Task<int> GetProductArticleByIdAsync(int id);

        Task UpdateAsync(ProductForUpdateDto model);

        Task DeleteProductAsync(int id);

        Task<int> GetProductIdByArticleAsync(int article);

        Task<ProductEntity> GetProductByIdAsync(int id);

        Task<CountProductsDto> GetProductsCountAsync(string userId);
    }
}
