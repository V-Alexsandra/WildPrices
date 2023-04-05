using WildPrices.Data.Entities;

namespace WildPrices.Data.Repositories.Contracts
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    {
        Task<ProductEntity> CreateWhenDoesNotExistAsync(ProductEntity entity);

        Task<IEnumerable<ProductEntity>> GetAllIsDesiredAsync();

        Task<IEnumerable<ProductEntity>> GetAllIsNotDesiredAsync();

        Task<bool> GetIsDesiredPriceByArticleAsync(int article);

        Task<int> GetArticleById(int id);
    }
}
