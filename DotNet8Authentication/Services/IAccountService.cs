using DotNet8Authentication.Dtos;
using DotNet8Authentication.Models;

namespace DotNet8Authentication.Services;

public interface IAccountService
{
    Task<RegisterResponseDto> Register(RegisterModel model);
    Task<LoginResponseDto> Login(LoginModel model);
    Task<RefreshTokenResponseDto> RefreshToken(RefreshTokenRequestModel model);
}