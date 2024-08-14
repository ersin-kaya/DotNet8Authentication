using DotNet8Authentication.Constants;
using DotNet8Authentication.Dtos;
using DotNet8Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace DotNet8Authentication.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<RegisterResponseDto> Register(RegisterModel model)
    {
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
            return new RegisterResponseDto
            {
                Succeeded = false,
                Message = Messages.EmailAlreadyInUse
            };

        var user = new ApplicationUser(model.UserName)
        {
            Email = model.Email,
            FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            var response = new RegisterResponseDto
            {
                Succeeded = true,
                Message = Messages.RegistrationSuccess,
                CreatedUser = new CreatedUserDto<string>
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FullName = user.FullName
                },
            };
            return response;
        }
        
        var errors = result.Errors.Select(e => e.Description);
        return new RegisterResponseDto
        {
            Succeeded = false,
            ErrorMessages = errors
        };
    }
}