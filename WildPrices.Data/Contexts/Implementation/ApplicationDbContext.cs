using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WildPrices.Data.Contexts.Contracts;
using WildPrices.Data.Entities;

namespace WildPrices.Data.Contexts.Implementation
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<PriceHistoryEntity> PriceHistories { get; set; } = null!;
        public DbSet<ProductEntity> Products { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();
    }
}
