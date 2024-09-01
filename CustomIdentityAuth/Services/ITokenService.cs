using System.Security.Claims;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface ITokenService
{
    Task<IServiceResult<UserToken>> GenerateJwtToken(ApplicationUser user);
    Task<IServiceResult<UserToken>> GenerateRefreshToken(ApplicationUser user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}