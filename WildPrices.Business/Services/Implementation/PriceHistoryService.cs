using AutoMapper;
using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.Services.Common;
using WildPrices.Data.Entities;
using WildPrices.Data.Repositories.Contracts;

namespace WildPrices.Business.Services.Implementation
{
    public class PriceHistoryService : IPriceHistoryService
    {
        private IMapper _mapper;
        private IPriceHistoryRepository _priceHistoryRepository;

        public PriceHistoryService(IMapper mapper, IPriceHistoryRepository priceHistoryRepository)
        {
            _mapper = mapper;
            _priceHistoryRepository = priceHistoryRepository;
        }

        public async Task CreatePriceHistoryAsync(PriceHistoryForCreationDto model)
        {
            var priceHistory = _mapper.Map<PriceHistoryEntity>(model);

            await _priceHistoryRepository.CreateAsync(priceHistory);
        }

        public async Task DeleteAllByProductIdAsync(int productId)
        {
            await _priceHistoryRepository.DeleteAllByProductIdAsync(productId);
        }

        public async Task<IEnumerable<PriceHistoryEntity>> GetAllPriceHistoryByProductIdAsync(int productId)
        {
            return await _priceHistoryRepository.GetAllByProductIdAsync(productId);
        }

        public async Task<MinAndMaxPriceDto> GetMaxAndMinPriceAsync(int productId)
        {
            var maxPrice = await _priceHistoryRepository.GetMaxPriceAsync(productId);
            var minPrice = await _priceHistoryRepository.GetMinPriceAsync(productId);

            return new MinAndMaxPriceDto
            {
                MaxPrice = maxPrice,
                MinPrice = minPrice
            };
        }

        public async Task<double> GetCurrentPriceAsync(int productId)
        {
            return await _priceHistoryRepository.GetTheCurrentPriceAsync(productId);
        }
    }
}
