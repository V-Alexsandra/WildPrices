using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WildPrices.Business.DTOs;
using WildPrices.Business.Services.Common;

namespace WildPrices.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
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
