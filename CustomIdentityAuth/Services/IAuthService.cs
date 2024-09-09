using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface IAuthService
{
    Task<IServiceResult> Register(RegisterRequestDto requestDto);
    Task<IServiceResult<LoginResponseDto>> Login(LoginRequestDto requestDto);
    Task<IServiceResult<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestDto requestDto);
}