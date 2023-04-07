using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.Services.Common
{
    public interface IPriceHistoryService
    {
        Task CreatePriceHistoryAsync(ProductFromWildberriesDto model);

        Task DeleteAllByProductIdAsync(int productId);

        Task<MinAndMaxPriceDto> GetMaxAndMinPriceAsync(int productId);

        Task<IEnumerable<PriceHistoryEntity>> GetAllPriceHistoryByProductIdAsync(int productId);
    }
}
