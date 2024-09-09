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

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ISettingsService _settingsService;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService, ISettingsService settingsService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _settingsService = settingsService;
    }

    #region Public Service Methods

    public async Task<IServiceResult> Register(RegisterRequestDto requestDto)
    {
        var userByEmail = await _userManager.FindByEmailAsync(requestDto.Email);
        if (userByEmail != null)
            return ServiceResult.Failure(errorMessage: Messages.EmailAlreadyInUse, message: Messages.RegistrationError);

        var user = requestDto.MapToApplicationUserForRegister();
        var createUserResult = await _userManager.CreateAsync(user, requestDto.Password);
        if(!createUserResult.Succeeded)
            return ServiceResult.Failure(
                errorMessages: createUserResult.Errors.Select(e => e.Description), 
                message: Messages.RegistrationError);
        
        var roleResult = await _userManager.AddToRoleAsync(user, RoleConstants.User);
        if (!roleResult.Succeeded)
            return ServiceResult.Failure(
                errorMessages: roleResult.Errors.Select(e => e.Description),
                message: Messages.RoleAssignmentError);
        
        // var registrationResponse = new RegisterResponseDto
        // {
        //     CreatedUser = user.MapToCreatedUserDto(roles: await _userManager.GetRolesAsync(user))
        // };
        
        return ServiceResult.Success(message: Messages.RegistrationSuccess);
    }

    public async Task<IServiceResult<LoginResponseDto>> Login(LoginRequestDto requestDto)
    {
        var user = await _userManager.FindByEmailAsync(requestDto.Email);
        if (user == null)
            return ServiceResult<LoginResponseDto>.Failure(errorMessage: Messages.InvalidLoginAttempt, message: Messages.LoginError);

        var result = await _signInManager.CheckPasswordSignInAsync(user, requestDto.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
            return ServiceResult<LoginResponseDto>.Failure(errorMessage: Messages.InvalidLoginAttempt, message: Messages.LoginError);

        var tokenGenerationResult = await CreateUserTokenAsync(user);
        if (!tokenGenerationResult.Succeeded)
            return ServiceResult<LoginResponseDto>.Failure(errorMessage:Messages.TokenGenerationFailed, message:Messages.LoginError);
        
        var userToken = tokenGenerationResult.Data;
        user.RefreshToken = userToken.RefreshToken;
        user.RefreshTokenExpiration = DateTime.Now.AddDays(_settingsService.TokenSettings.RefreshTokenExpirationDays);
        
        var updateUserResult = await _userManager.UpdateAsync(user);
        if (!updateUserResult.Succeeded) // Should be handled transactionally
            return ServiceResult<LoginResponseDto>.Failure(
                errorMessages: updateUserResult.Errors.Select(e => e.Description), 
                message: Messages.UserUpdateFailed);

        var updateSecurityStampResult = await _userManager.UpdateSecurityStampAsync(user);
        if (!updateSecurityStampResult.Succeeded) // Should be handled transactionally
            return ServiceResult<LoginResponseDto>.Failure(
                errorMessages: updateSecurityStampResult.Errors.Select(e => e.Description), 
                message: Messages.SecurityStampUpdateFailed); 
        
        userToken.ExpiresAt = DateTime.Now.AddMinutes(_settingsService.TokenSettings.ExpirationMinutes);

        var loginResult = userToken.MapToLoginResponseDto();
        return ServiceResult<LoginResponseDto>.Success(data:loginResult, message:Messages.LoginSuccess);
    }

    public async Task<IServiceResult<RefreshTokenResponseDto>> RefreshToken(RefreshTokenRequestDto requestDto)
    {
        ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(requestDto.AccessToken).Data;
        string? email = principal.FindFirstValue(ClaimTypes.Email);

        ApplicationUser user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return ServiceResult<RefreshTokenResponseDto>.Failure(errorMessage:Messages.InvalidLoginAttempt, message:Messages.RefreshTokenFailed);

        if (user.RefreshToken != requestDto.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
            throw new SecurityTokenException(Messages.InvalidRefreshToken);
        
        var updatedUserToken = (await CreateUserTokenAsync(user)).Data;
        user.RefreshToken = updatedUserToken.RefreshToken;
        user.RefreshTokenExpiration = DateTime.Now.AddDays(_settingsService.TokenSettings.RefreshTokenExpirationDays);
        
        var updateUserResult = await _userManager.UpdateAsync(user);
        if (!updateUserResult.Succeeded) // Should be handled transactionally
            return ServiceResult<RefreshTokenResponseDto>.Failure(
                errorMessages:updateUserResult.Errors.Select(e => e.Description), 
                message:Messages.UserUpdateFailed);
        
        var updateSecurityStampResult = await _userManager.UpdateSecurityStampAsync(user);
        if (!updateSecurityStampResult.Succeeded) // Should be handled transactionally
            return ServiceResult<RefreshTokenResponseDto>.Failure(
                errorMessages: updateSecurityStampResult.Errors.Select(e => e.Description), 
                message: Messages.SecurityStampUpdateFailed); 

        var accessTokenExpiresAt = DateTime.Now.AddMinutes(_settingsService.TokenSettings.ExpirationMinutes);
        
        var refreshTokenResult = updatedUserToken.MapToRefreshTokenResponseDto(accessTokenExpiresAt);
        return ServiceResult<RefreshTokenResponseDto>.Success(data:refreshTokenResult, message:Messages.RefreshTokenSuccess);
    }

    #endregion
    
    #region Helper Methods

    private async Task<IServiceResult<UserToken>> CreateUserTokenAsync(ApplicationUser user)
    {
        var userToken = await _tokenService.GenerateJwtToken(user);
        userToken.Data.RefreshToken = await _tokenService.GetRefreshTokenAsync(user);
        
        return ServiceResult<UserToken>.Success(data:userToken.Data);
    }

    #endregion
}