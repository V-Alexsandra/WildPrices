using Microsoft.AspNetCore.Identity;
using WildPrices.Business.DTOs;
using WildPrices.Business.Exceptions;
using WildPrices.Business.Services.Common;
using WildPrices.Data.Entities;

namespace WildPrices.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<UserEntity> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<RegisterSuccessDto> RegisterUserAsync(RegisterUserDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Register Model is null");
            }

            if (model.Password != model.RepeatPassword)
            {
                throw new NotSucceededException("Incorrect Password");
            }

            var identityUser = new UserEntity
            {
                Email = model.Email,
                UserName = "NewUser"
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (!result.Succeeded)
            {
                throw new NotSucceededException("Register failed");
            }

            return new RegisterSuccessDto
            {
                Email = identityUser.Email
            };
        }

        public async Task<LoginSuccessDto> LoginUserAsync(LoginUserDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "LoginUserDto Model is null");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                throw new NotSucceededException("Invalid password");
            }

            return new LoginSuccessDto
            {
                Token = _tokenService.GenerateAccessToken(await _tokenService.GetClaimsAsync(user.Email))
            };
        }
    }
}
