using AutoMapper;
using WildPrices.Business.DTOs;
using WildPrices.Data.Entities;

namespace WildPrices.Business.MappingProfiles
{
    public class ProductCardProfile : Profile
    {
        public ProductCardProfile()
        {
            CreateMap<ProductEntity, ProductCardDto>()
                .ForMember(dest => dest.IsDesiredPrice, opt => opt.MapFrom(src => src.IsDesiredPrice ? "достиг желаемой стоимости" : "не достиг желаемой стоимости"))
                .ForMember(dest => dest.CurrentPrice, opt => opt.Ignore());

            CreateMap<CurrentPriceDto, ProductDto>()
                .ForMember(dest => dest.IsDesiredPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Link, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.DesiredPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Article, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(src => src.CurrentPrice));
        }
    }
}
