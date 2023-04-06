using AutoMapper;
using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.MappingProfiles
{
    public class ProductCardProfile : Profile
    {
        public ProductCardProfile()
        {
            CreateMap<ProductEntity, ProductCardForViewDto>()
                .ForMember(dest => dest.IsDesiredPrice, opt => opt.MapFrom(src => src.IsDesiredPrice ? "достиг желаемой стоимости" : "не достиг желаемой стоимости"))
                .ForMember(dest => dest.CurrentPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CurrentPriceDto, ProductForViewDto>()
                .ForMember(dest => dest.IsDesiredPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Link, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.DesiredPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Article, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(src => src.CurrentPrice))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
