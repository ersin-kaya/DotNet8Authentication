using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;

namespace CustomIdentityAuth.Mappers;

public static class TokenMapper
{
    public static LoginResponseDto MapToLoginResponseDto(this UserToken userToken)
    {
        return new LoginResponseDto { UserToken = userToken };
    }
    
    public static RefreshTokenResponseDto MapToRefreshTokenResponseDto(this UserToken userToken, DateTime accessTokenExpiresAt)
    {
        return new RefreshTokenResponseDto
        {
            AccessToken = userToken.AccessToken,
            RefreshToken = userToken.RefreshToken,
            ExpiresAt = accessTokenExpiresAt
        };
    }
}