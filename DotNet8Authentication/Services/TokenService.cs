using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNet8Authentication.Models;
using DotNet8Authentication.Services.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DotNet8Authentication.Services;

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
            //todo: add refresh token
        };
    }
}