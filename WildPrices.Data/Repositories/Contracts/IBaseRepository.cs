﻿using WildPrices.Data.Entities;

namespace WildPrices.Data.Repositories.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
