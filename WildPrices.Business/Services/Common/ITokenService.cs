using System.Security.Claims;

namespace WildPrices.Business.Services.Common
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        Task<IEnumerable<Claim>> GetClaimsAsync(string email);
    }
}
