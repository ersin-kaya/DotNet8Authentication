using System.Security.Claims;
using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface ITokenService
{
    Task<IServiceResult<AuthTokenDto>> GenerateJwtToken(ApplicationUser user);
    Task<IServiceResult<AuthTokenDto>> GenerateRefreshToken(ApplicationUser user);
    IServiceResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}