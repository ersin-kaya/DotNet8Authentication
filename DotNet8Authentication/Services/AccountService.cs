using DotNet8Authentication.Constants;
using DotNet8Authentication.Dtos;
using DotNet8Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace DotNet8Authentication.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
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

        return new LoginResponseDto
        {
            Succeeded = true,
            Message = Messages.LoginSuccess,
            UserToken = userToken
        };
    }
}