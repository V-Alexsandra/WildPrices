using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WildPrices.Business.DTOs.UserDtos;
using WildPrices.Business.Services.Common;
using WildPrices.WebApi.Controllers.Contracts;

namespace WildPrices.WebApi.Controllers.Implementation
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase, IAuthController
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;

        public AuthController(IUserService userService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
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
            _memoryCache.Set("UserId", login.Id);
            return Ok(login);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> ChangeUserName([FromBody] string userName)
        {
            if (_memoryCache.TryGetValue("UserId", out string? id))
            {
                await _userService.ChangeUserName(userName, id);
            }

            return Ok(userName);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            if (_memoryCache.TryGetValue("UserId", out string? id))
            {
                var profile = await _userService.GetUserProfile(id);
                return Ok(profile);
            }

            return NotFound();
        }
    }
}
