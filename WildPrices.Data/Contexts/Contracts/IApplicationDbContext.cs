using Microsoft.EntityFrameworkCore;
using WildPrices.Data.Entities;

namespace WildPrices.Data.Contexts.Contracts
{
    public interface IApplicationDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        public DbSet<PriceHistoryEntity> PriceHistories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
