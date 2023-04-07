using AutoMapper;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.MappingProfiles
{
    public class PriceHistoryProfile : Profile
    {
        public PriceHistoryProfile()
        {
            CreateMap<ProductFromWildberriesDto, PriceHistoryEntity>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                 .ForMember(dest => dest.ProductEntity, opt => opt.Ignore()); 
            //for view
        }
    }
}
