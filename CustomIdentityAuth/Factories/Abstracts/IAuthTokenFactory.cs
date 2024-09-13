using CustomIdentityAuth.Dtos.Concretes;

namespace CustomIdentityAuth.Factories.Abstracts;

public interface IAuthTokenFactory : IFactory
{
    AuthTokenDto Create(string? accessToken = default, string? refreshToken = default, DateTimeOffset expiresAt = default);
    AuthTokenDto CreateForAccessToken(string accessToken, DateTimeOffset expiresAt);
    AuthTokenDto CreateForRefreshToken(string refreshToken);
}