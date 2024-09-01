using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;

namespace CustomIdentityAuth.Services;

public interface IAccountService
{
    Task<RegisterResponseDto> Register(RegisterModel model);
    Task<LoginResponseDto> Login(LoginModel model);
    Task<RefreshTokenResponseDto> RefreshToken(RefreshTokenRequestModel model);
}