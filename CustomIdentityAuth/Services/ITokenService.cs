using System.Security.Claims;
using CustomIdentityAuth.Models;

namespace CustomIdentityAuth.Services;

public interface ITokenService
{
    Task<UserToken> GenerateJwtToken(ApplicationUser user);
    Task<UserToken> GenerateRefreshToken(ApplicationUser user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}