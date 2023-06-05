using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WildPrices.Data.Contexts.Contracts;
using WildPrices.Data.Entities;
using WildPrices.Data.Repositories.Contracts;

namespace WildPrices.Data.Repositories.Implementation
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(IApplicationDbContext appContext) : base(appContext)
        {
        }

        public async Task<ProductEntity> CreateWhenDoesNotExistAsync(ProductEntity entity)
        {
            var existingEntity = await DbSet.FindAsync(entity.Article);

            if (existingEntity != null)
            {
                return existingEntity;
            }

            await DbSet.AddAsync(entity);
            await appContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsyncByUserId(string userId) =>
           await DbSet
          .Where(e => e.UserId == userId)
          .ToListAsync();


        public async Task<IEnumerable<ProductEntity>> GetAllIsDesiredAsync(string userId) =>
            await DbSet
            .Where(e => e.IsDesiredPrice)
            .Where(e => e.UserId == userId)
            .ToListAsync();

        public async Task<IEnumerable<ProductEntity>> GetAllIsNotDesiredAsync(string userId) =>
            await DbSet
            .Where(e => !e.IsDesiredPrice)
            .Where(e => e.UserId == userId)
            .ToListAsync();

        public async Task<bool> GetIsDesiredPriceByArticleAsync(int article)
        {
            var entity = await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Article == article);

            return entity.IsDesiredPrice;
        }

        public async Task<int> GetArticleByIdAsync(int id)
        {
            var entity = await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

            return entity.Article;
        }

        public async Task<int> GetProductIdByArticle(int article)
        {
            var entity = await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Article == article);

            return entity.Id;
        }
        public async Task<double> GetDesiredPriceByArticleAsync(int article)
        {
            var product = await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Article == article);

            return product.DesiredPrice;
        }
    } 
}
