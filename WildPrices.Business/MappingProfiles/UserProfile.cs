using AutoMapper;
using WildPrices.Business.DTOs.UserDtos;
using WildPrices.Data.Entities;

namespace WildPrices.Business.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDto>()
                 .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }
}
