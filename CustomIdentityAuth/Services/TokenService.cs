using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CustomIdentityAuth.Constants;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Services.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CustomIdentityAuth.Services;

public class TokenService : ITokenService
{
    private readonly ISettingsService _settingsService;
    private readonly UserManager<ApplicationUser> _userManager;

    public TokenService(UserManager<ApplicationUser> userManager, ISettingsService settingsService)
    {
        _userManager = userManager;
        _settingsService = settingsService;
    }

    public async Task<UserToken> GenerateJwtToken(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserName)
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsService.TokenSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settingsService.TokenSettings.Issuer,
            audience: _settingsService.TokenSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_settingsService.TokenSettings.ExpirationMinutes),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new UserToken
        {
            AccessToken = tokenString,
            ExpiresAt = token.ValidTo
        };
    }

    public Task<UserToken> GenerateRefreshToken(ApplicationUser user)
    {
        var randomNumber = new byte[64];
        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);

        var refreshToken = new UserToken
        {
            RefreshToken = Convert.ToBase64String(randomNumber)
        };
        
        return Task.FromResult(refreshToken);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _settingsService.TokenSettings.Issuer,
            ValidAudience = _settingsService.TokenSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsService.TokenSettings.Key)),
            ClockSkew = TimeSpan.Zero // This sets the time tolerance for JWT validation to zero, meaning the token must be valid exactly within the specified time frame, with no deviations allowed.
        };

        JwtSecurityTokenHandler tokenHandler = new();
        ClaimsPrincipal principal =
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase)
           )
            throw new SecurityTokenException(Messages.InvalidToken);

        return principal;
    }
}