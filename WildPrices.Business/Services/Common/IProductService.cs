using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.Services.Common
{
    public interface IProductService
    {
        DesiredPriceDto GetDisiredPrice(double desiredPrice);

        Task CreateProductWhenDoesNotExistAsync(ProductFromWildberriesDto model, DesiredPriceDto desiredPriceDto, string id);

        Task<IEnumerable<ProductEntity>> GetAllProductAsync(string userId);

        Task<IEnumerable<ProductEntity>> GetAllIsDesiredProductsAsync();

        Task<IEnumerable<ProductEntity>> GetAllIsNotDesiredProductsAsync();

        Task<int> GetProductArticleByIdAsync(int id);

        Task UpdateAsync(ProductForUpdateDto model);

        Task DeleteProductAsync(int id);

        Task<int> GetProductIdByArticle(int article);

        Task<ProductEntity> GetProductById(int id);
    }
}
