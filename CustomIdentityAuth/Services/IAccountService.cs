using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface IAccountService
{
    Task<IServiceResult<RegisterResponseDto>> Register(RegisterModel model);
    Task<LoginResponseDto> Login(LoginModel model);
    Task<RefreshTokenResponseDto> RefreshToken(RefreshTokenRequestModel model);
}