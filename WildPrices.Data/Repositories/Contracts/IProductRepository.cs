using WildPrices.Data.Entities;

namespace WildPrices.Data.Repositories.Contracts
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    {
        Task<ProductEntity> CreateWhenDoesNotExistAsync(ProductEntity entity);
    }
}
