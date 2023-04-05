using WildPrices.Business.DTOs;

namespace WildPrices.Business.Services.Common
{
    public interface IUserService
    {
        Task<RegisterSuccessDto> RegisterUserAsync(RegisterUserDto model);
        Task<LoginSuccessDto> LoginUserAsync(LoginUserDto model);
    }
}
