using WildPrices.Business.DTOs.UserDtos;

namespace WildPrices.Business.Services.Common
{
    public interface IUserService
    {
        Task<RegisterSuccessDto> RegisterUserAsync(RegisterUserDto model);

        Task<LoginSuccessDto> LoginUserAsync(LoginUserDto model);

        Task ChangeUserName(string userName, string id);
    }
}
