using AutoMapper;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductForCreationDto, ProductEntity>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.UserId, opt => opt.Ignore())
                 .ForMember(dest => dest.UserEntity, opt => opt.Ignore());

            //добавить маппинг для forview

        }
    }
}
