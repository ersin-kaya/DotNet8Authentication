using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface IAccountService
{
    Task<IServiceResult> Register(RegisterModel model);
    Task<IServiceResult<LoginResponseDto>> Login(LoginModel model);
    Task<IServiceResult<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestModel model);
}