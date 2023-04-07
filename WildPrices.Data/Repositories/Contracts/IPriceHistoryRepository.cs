using WildPrices.Data.Entities;

namespace WildPrices.Data.Repositories.Contracts
{
    public interface IPriceHistoryRepository : IBaseRepository<PriceHistoryEntity>
    {
        Task DeleteAllByProductIdAsync(int productId);

        Task<IEnumerable<PriceHistoryEntity>> GetAllByProductIdAsync(int productId);

        Task<double> GetTheCurrentPriceAsync(int productId);

        Task<double> GetMaxPriceAsync(int productId);

        Task<double> GetMinPriceAsync(int productId);
    }
}
