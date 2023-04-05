using AutoMapper;
using WildPrices.Business.DTOs;
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
