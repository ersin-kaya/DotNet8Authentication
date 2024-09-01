using System.Security.Claims;
using DotNet8Authentication.Constants;
using DotNet8Authentication.Dtos;
using DotNet8Authentication.Models;
using DotNet8Authentication.Services.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DotNet8Authentication.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ISettingsService _settingsService;

    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, ISettingsService settingsService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _settingsService = settingsService;
    }
    
    public async Task<RegisterResponseDto> Register(RegisterModel model)
    {
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
            return new RegisterResponseDto
            {
                Succeeded = false,
                ErrorMessages = new[] { Messages.EmailAlreadyInUse },
                Message = Messages.RegistrationError
            };

        var user = new ApplicationUser(model.UserName)
        {
            Email = model.Email,
            FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, RoleConstants.User);

            if (!roleResult.Succeeded)
            {
                var roleErrors = roleResult.Errors.Select(e => e.Description);
                return new RegisterResponseDto
                {
                    Succeeded = false,
                    ErrorMessages = roleErrors,
                    Message = Messages.RoleAssignmentError
                };
            }
            
            // var userToken = await _tokenService.GenerateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            
            var response = new RegisterResponseDto
            {
                Succeeded = true,
                Message = Messages.RegistrationSuccess,
                CreatedUser = new CreatedUserDto<string>
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Roles = roles
                },
                //UserToken = userToken
            };
            return response;
        }
        
        var errors = result.Errors.Select(e => e.Description);
        return new RegisterResponseDto
        {
            Succeeded = false,
            ErrorMessages = errors,
            Message = Messages.RegistrationError
        };
    }

    public async Task<LoginResponseDto> Login(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return new LoginResponseDto
            {
                Succeeded = false,
                ErrorMessages = new[] { Messages.InvalidLoginAttempt },
                Message = Messages.LoginError
            };

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
            return new LoginResponseDto
            {
                Succeeded = false,
                ErrorMessages = new[] { Messages.InvalidLoginAttempt },
                Message = Messages.LoginError
            };

        var userToken = await _tokenService.GenerateJwtToken(user);
        string generatedRefreshToken = (await _tokenService.GenerateRefreshToken(user)).RefreshToken;
        userToken.RefreshToken = generatedRefreshToken;

        user.RefreshToken = generatedRefreshToken;
        user.RefreshTokenExpiration = DateTime.Now.AddDays(_settingsService.TokenSettings.RefreshTokenExpirationDays);
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded) // Should be handled transactionally
            return new LoginResponseDto
            {
                Succeeded = false,
                ErrorMessages = updateResult.Errors.Select(e => e.Description).ToArray(),
                Message = Messages.UserUpdateFailed
            };

        await _userManager.UpdateSecurityStampAsync(user);
        
        var accessTokenExpiresAt = DateTime.Now.AddMinutes(_settingsService.TokenSettings.ExpirationMinutes);
        userToken.ExpiresAt = accessTokenExpiresAt;
        
        return new LoginResponseDto
        {
            Succeeded = true,
            Message = Messages.LoginSuccess,
            UserToken = userToken
        };
    }
    
    public async Task<RefreshTokenResponseDto> RefreshToken(RefreshTokenRequestModel model)
    {
        ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
        string? email = principal.FindFirstValue(ClaimTypes.Email);
        
        ApplicationUser user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return new RefreshTokenResponseDto
            {
                Succeeded = false,
                ErrorMessages = new[] { Messages.InvalidLoginAttempt },
                Message = Messages.RefreshTokenFailed
            };
        
        if (user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
            throw new SecurityTokenException(Messages.InvalidRefreshToken);

        string updatedAccessToken = (await _tokenService.GenerateJwtToken(user)).AccessToken;
        string updatedRefreshToken = (await _tokenService.GenerateRefreshToken(user)).RefreshToken;
        
        user.RefreshToken = updatedRefreshToken;
        user.RefreshTokenExpiration = DateTime.Now.AddDays(_settingsService.TokenSettings.RefreshTokenExpirationDays);
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded) // Should be handled transactionally
            return new RefreshTokenResponseDto
            {
                Succeeded = false,
                ErrorMessages = updateResult.Errors.Select(e => e.Description).ToArray(),
                Message = Messages.UserUpdateFailed
            };
        
        await _userManager.UpdateSecurityStampAsync(user);
        
        var accessTokenExpiresAt = DateTime.Now.AddMinutes(_settingsService.TokenSettings.ExpirationMinutes);

        return new RefreshTokenResponseDto
        {
            AccessToken = updatedAccessToken,
            RefreshToken = updatedRefreshToken,
            ExpiresAt = accessTokenExpiresAt
        };
    }
}