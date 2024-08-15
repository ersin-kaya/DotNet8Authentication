using DotNet8Authentication.Models;

namespace DotNet8Authentication.Services;

public interface ITokenService
{
    Task<UserToken> GenerateJwtToken(ApplicationUser user);
}