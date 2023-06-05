using AutoMapper;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.DTOs.UserDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductForCreationDto, ProductEntity>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.UserEntity, opt => opt.Ignore());

            CreateMap<ProductForUpdateDto, ProductEntity>()
                .ForMember(dest => dest.UserEntity, opt => opt.Ignore());

            CreateMap<ProductEntity, ProductCardForViewDto>()
                 .ForMember(dest => dest.CurrentPrice, opt => opt.Ignore());
        }
    }
}
