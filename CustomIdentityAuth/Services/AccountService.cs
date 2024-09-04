using System.Security.Claims;
using CustomIdentityAuth.Constants;
using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Extensions;
using CustomIdentityAuth.Mappers;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Results;
using CustomIdentityAuth.Services.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CustomIdentityAuth.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ISettingsService _settingsService;

    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService, ISettingsService settingsService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _settingsService = settingsService;
    }

    #region Public Service Methods

    public async Task<IServiceResult<RegisterResponseDto>> Register(RegisterModel model)
    {
        var userByEmail = await _userManager.FindByEmailAsync(model.Email);
        if (userByEmail != null)
            return ServiceResult<RegisterResponseDto>.Failure(errorMessage: Messages.EmailAlreadyInUse, message: Messages.RegistrationError);

        var user = model.CreateApplicationUserForRegister();
        var createUserResult = await _userManager.CreateAsync(user, model.Password);
        if(!createUserResult.Succeeded)
            return ServiceResult<RegisterResponseDto>.Failure(
                errorMessages: createUserResult.Errors.Select(e => e.Description), 
                message: Messages.RegistrationError);
        
        var roleResult = await _userManager.AddToRoleAsync(user, RoleConstants.User);
        if (!roleResult.Succeeded)
            return ServiceResult<RegisterResponseDto>.Failure(
                errorMessages: roleResult.Errors.Select(e => e.Description),
                message: Messages.RoleAssignmentError);
        
        var registrationResult = await CreateRegistrationResponse(user);
        return ServiceResult<RegisterResponseDto>.Success(data: registrationResult, message: Messages.RegistrationSuccess);
    }

    public async Task<IServiceResult<LoginResponseDto>> Login(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return ServiceResult<LoginResponseDto>.Failure(errorMessage: Messages.InvalidLoginAttempt, message: Messages.LoginError);

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
            return ServiceResult<LoginResponseDto>.Failure(errorMessage: Messages.InvalidLoginAttempt, message: Messages.LoginError);

        var tokenGenerationResult = await BuildUserTokenAsync(user);
        if (!tokenGenerationResult.Succeeded)
            return ServiceResult<LoginResponseDto>.Failure(errorMessage:Messages.TokenGenerationFailed, message:Messages.LoginError);
        (user, var userToken) = tokenGenerationResult.Data;
        
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded) // Should be handled transactionally
            return ServiceResult<LoginResponseDto>.Failure(
                errorMessages: updateResult.Errors.Select(e => e.Description), 
                message: Messages.UserUpdateFailed);

        var updateSecurityStampResult = await _userManager.UpdateSecurityStampAsync(user);
        if (!updateSecurityStampResult.Succeeded) // Should be handled transactionally
            return ServiceResult<LoginResponseDto>.Failure(
                errorMessages: updateSecurityStampResult.Errors.Select(e => e.Description), 
                message: Messages.SecurityStampUpdateFailed); 
        
        userToken.ExpiresAt = DateTime.Now.AddMinutes(_settingsService.TokenSettings.ExpirationMinutes);
        
        var loginResult = new LoginResponseDto { UserToken = userToken };
        return ServiceResult<LoginResponseDto>.Success(data:loginResult, message:Messages.LoginSuccess);
    }

    public async Task<IServiceResult<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestModel model)
    {
        ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken).Data;
        string? email = principal.FindFirstValue(ClaimTypes.Email);

        ApplicationUser user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return ServiceResult<RefreshTokenResponseDto>.Failure(errorMessage:Messages.InvalidLoginAttempt, message:Messages.RefreshTokenFailed);

        if (user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
            throw new SecurityTokenException(Messages.InvalidRefreshToken);

        string updatedAccessToken = await _tokenService.GetAccessTokenAsync(user);
        string updatedRefreshToken = await _tokenService.GetRefreshTokenAsync(user);

        user.RefreshToken = updatedRefreshToken;
        user.RefreshTokenExpiration = DateTime.Now.AddDays(_settingsService.TokenSettings.RefreshTokenExpirationDays);
        
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded) // Should be handled transactionally
        {
            var errorMessages = updateResult.Errors.Select(e => e.Description).ToArray();
            return ServiceResult<RefreshTokenResponseDto>.Failure(errorMessages:errorMessages, message:Messages.UserUpdateFailed);
        }

        await _userManager.UpdateSecurityStampAsync(user);

        var accessTokenExpiresAt = DateTime.Now.AddMinutes(_settingsService.TokenSettings.ExpirationMinutes);
        
        var refreshTokenResult = new RefreshTokenResponseDto
        {
            AccessToken = updatedAccessToken,
            RefreshToken = updatedRefreshToken,
            ExpiresAt = accessTokenExpiresAt
        };
        return ServiceResult<RefreshTokenResponseDto>.Success(data:refreshTokenResult, message:Messages.RefreshTokenSuccess);
    }

    #endregion
    
    #region Helper Methods
    
    private async Task<RegisterResponseDto> CreateRegistrationResponse(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return new RegisterResponseDto
        {
            CreatedUser = new CreatedUserDto<string>
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                Roles = roles
            },
            // UserToken = await _tokenService.GenerateJwtToken(user)
        };
    }

    private async Task<IServiceResult<(ApplicationUser User, UserToken Token)>> BuildUserTokenAsync(ApplicationUser user)
    {
        var userToken = await _tokenService.GenerateJwtToken(user);
        string generatedRefreshToken = await _tokenService.GetRefreshTokenAsync(user);
        userToken.Data.RefreshToken = generatedRefreshToken;
        
        user.RefreshToken = generatedRefreshToken;
        user.RefreshTokenExpiration = DateTime.Now.AddDays(_settingsService.TokenSettings.RefreshTokenExpirationDays);
        
        return ServiceResult<(ApplicationUser User, UserToken Token)>.Success(data:(user, userToken.Data));
    }

    #endregion
}