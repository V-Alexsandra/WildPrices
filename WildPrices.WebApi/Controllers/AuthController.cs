using Microsoft.AspNetCore.Mvc;
using WildPrices.Business.DTOs.UserDtos;
using WildPrices.Business.Services.Common;

namespace WildPrices.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto model)
        {
            var register = await _userService.RegisterUserAsync(model);

            return Ok(register);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto model)
        {
            var login = await _userService.LoginUserAsync(model);

            return Ok(login);
        }
    }
}
