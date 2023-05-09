using Microsoft.AspNetCore.Mvc;
using WildPrices.Business.DTOs.UserDtos;

namespace WildPrices.WebApi.Controllers.Contracts
{
    public interface IAuthController
    {
        Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto model);

        Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto model);

        Task<IActionResult> ChangeUserName([FromBody] string userName);
    }
}
