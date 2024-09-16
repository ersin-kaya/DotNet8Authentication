using System.Security.Claims;
using CustomIdentityAuth.Dtos.Concretes;
using CustomIdentityAuth.Entities;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface ITokenService
{
    Task<IServiceResult<AccessTokenDto>> GenerateJwtToken(ApplicationUser user);
    Task<IServiceResult<RefreshTokenDto>> GenerateRefreshToken(ApplicationUser user);
    IServiceResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}