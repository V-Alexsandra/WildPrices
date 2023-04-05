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

        public async Task<double> GetTheCurrentPriceAsync(int productId)
        {
            var entities = await DbSet
           .Where(e => e.ProductId == productId)
           .ToListAsync();

            var last = entities.Last();

            return last.CurrentPrice;
        }
    }
}
