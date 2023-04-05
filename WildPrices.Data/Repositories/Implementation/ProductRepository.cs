using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using System;
using WildPrices.Data.Contexts.Contracts;
using WildPrices.Data.Entities;
using WildPrices.Data.Repositories.Contracts;

namespace WildPrices.Data.Repositories.Implementation
{
    public abstract class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
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
    }
}
