using Microsoft.EntityFrameworkCore;
using WildPrices.Data.Contexts.Contracts;
using WildPrices.Data.Entities;
using WildPrices.Data.Repositories.Contracts;

namespace WildPrices.Data.Repositories.Implementation
{
    public class PriceHistoryRepository : BaseRepository<PriceHistoryEntity>, IPriceHistoryRepository
    {
        public PriceHistoryRepository(IApplicationDbContext appContext) : base(appContext)
        {
        }

        public async Task DeleteAllByProductIdAsync(int productId)
        {
            var entities = await DbSet.Where(e => e.ProductId == productId).ToListAsync();
            DbSet.RemoveRange(entities);
            await appContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PriceHistoryEntity>> GetAllByProductIdAsync(int productId) =>
            await DbSet
            .Where(e => e.ProductId == productId)
            .ToListAsync();

        public async Task<double> GetMaxPriceAsync(int productId)
        {
            var maxPrice = await DbSet
                .Where(ph => ph.ProductId == productId)
                .MaxAsync(ph => ph.CurrentPrice);

            return maxPrice;
        }

        public async Task<double> GetMinPriceAsync(int productId)
        {
            var minPrice = await DbSet
               .Where(ph => ph.ProductId == productId)
               .MinAsync(ph => ph.CurrentPrice);

            return minPrice;
        }

        public async Task<double> GetTheCurrentPriceAsync(int productId)
        {
            var entities = await DbSet
                .Where(e => e.ProductId == productId)
                .ToListAsync();

            if (!entities.Any())
            {
                throw new InvalidOperationException("No price history found for the given product.");
            }

            var last = entities.Last();

            return last.CurrentPrice;
        }
    }
}
