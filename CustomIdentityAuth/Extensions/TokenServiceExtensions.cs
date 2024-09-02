using CustomIdentityAuth.Constants;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Services;

namespace CustomIdentityAuth.Extensions;

public static class TokenServiceExtensions
{
    /// <summary>
    /// Kullanıcı için JWT access token oluşturur ve döner.
    /// </summary>
    /// <param name="tokenService">ITokenService örneği.</param>
    /// <param name="user">Token oluşturulacak kullanıcı.</param>
    /// <returns>Access token string değeri.</returns>
    /// <exception cref="InvalidOperationException">Access token oluşturulamazsa.</exception>
    public static async Task<string> GetAccessTokenAsync(this ITokenService tokenService, ApplicationUser user)
    {
        if (tokenService == null)
            throw new ArgumentNullException(nameof(tokenService));

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var tokenResult = await tokenService.GenerateJwtToken(user);
        
        if (tokenResult?.Data?.AccessToken == null)
        {
            throw new InvalidOperationException(Messages.AccessTokenGenerationFailed);
        }

        return tokenResult.Data.AccessToken;
    }
}