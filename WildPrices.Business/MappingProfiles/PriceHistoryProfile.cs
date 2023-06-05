using AutoMapper;
using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.MappingProfiles
{
    public class PriceHistoryProfile : Profile
    {
        public PriceHistoryProfile()
        {
            CreateMap<PriceHistoryForCreationDto, PriceHistoryEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductEntity, opt => opt.Ignore());

            CreateMap<PriceHistoryEntity, PriceHistoryDto>();
        }
    }
}
