using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Business.DTOs.ProductDtos;

namespace WildPrices.Business.Services.Common
{
    public interface IPriceHistoryService
    {
        Task CreatePriceHistoryAsync(PriceHistoryDto model);

        Task<CurrentPriceDto> GetCurrentPriceForProductAsync(ProductForViewDto model);

        Task<CurrentPriceDto> GetCurrentPriceForProductCardAsync(ProductCardForViewDto model);

        Task<IEnumerable<PriceHistoryDto>> GetPriceHistoryForProductCardAsync(ProductCardForViewDto model);

        Task DeleteAllByProductIdAsync(ProductForViewDto model);
    }
}
