using CustomIdentityAuth.Dtos.Concretes;
using CustomIdentityAuth.Factories.Abstracts;

namespace CustomIdentityAuth.Factories.Concretes;

public class AuthTokenFactory : IAuthTokenFactory
{
    public AuthTokenDto Create(string? accessToken = default, string? refreshToken = default, DateTimeOffset expiresAt = default)
    {
        AuthTokenDto authTokenDto = new AuthTokenDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt
        };
        return authTokenDto;
    }

    public AuthTokenDto CreateForAccessToken(string accessToken, DateTimeOffset expiresAt)
    {
        AuthTokenDto authTokenDto = new AuthTokenDto()
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAt
        };
        return authTokenDto;
    }

    public AuthTokenDto CreateForRefreshToken(string refreshToken)
    {
        AuthTokenDto authTokenDto = new AuthTokenDto()
        {
            RefreshToken = refreshToken
        };
        return authTokenDto;
    }
}