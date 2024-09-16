using CustomIdentityAuth.Dtos.Concretes;

namespace CustomIdentityAuth.Mappers;

public static class TokenMapper
{
    public static LoginResponseDto MapToLoginResponseDto(this AuthTokenDto authTokenDto)
    {
        return new LoginResponseDto { AuthTokenDto = authTokenDto };
    }
    
    public static RefreshTokenResponseDto MapToRefreshTokenResponseDto(this AuthTokenDto authTokenDto, DateTime accessTokenExpiresAt)
    {
        return new RefreshTokenResponseDto
        {
            AccessToken = authTokenDto.AccessToken,
            RefreshToken = authTokenDto.RefreshToken,
            ExpiresAt = accessTokenExpiresAt
        };
    }
}