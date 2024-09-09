using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface IAuthService
{
    Task<IServiceResult> Register(RegisterRequestDto requestDto);
    Task<IServiceResult<LoginResponseDto>> Login(LoginModel model);
    Task<IServiceResult<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestModel model);
}