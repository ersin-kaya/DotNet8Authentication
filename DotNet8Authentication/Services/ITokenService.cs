using System.Security.Claims;
using DotNet8Authentication.Models;

namespace DotNet8Authentication.Services;

public interface ITokenService
{
    Task<UserToken> GenerateJwtToken(ApplicationUser user);
    Task<UserToken> GenerateRefreshToken(ApplicationUser user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}