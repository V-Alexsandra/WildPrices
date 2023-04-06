using AutoMapper;
using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity, ProductForViewDto>()
                .ForMember(dest => dest.IsDesiredPrice, opt => opt.MapFrom(src => src.IsDesiredPrice ? "достиг желаемой стоимости" : "не достиг желаемой стоимости"))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.DesiredPrice, opt => opt.MapFrom(src => src.DesiredPrice))
                .ForMember(dest => dest.Article, opt => opt.MapFrom(src => src.Article))
                .ForMember(dest => dest.CurrentPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<CurrentPriceDto, ProductForViewDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
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
