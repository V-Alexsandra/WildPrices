using AutoMapper;
using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.MappingProfiles
{
    public  class PriceHistoryProfile : Profile
    {
        public PriceHistoryProfile()
        {
            CreateMap<PriceHistoryEntity, PriceHistoryDto>();
        }
    }
}
