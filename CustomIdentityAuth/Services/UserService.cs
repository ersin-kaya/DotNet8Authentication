// using System.Security.Claims;
using AutoMapper;
using CustomIdentityAuth.Dtos.Concretes;
using CustomIdentityAuth.Entities.Concretes;
using CustomIdentityAuth.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;

namespace CustomIdentityAuth.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    // private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IMapper mapper/*, IHttpContextAccessor httpContextAccessor*/)
    {
        _userManager = userManager;
        _mapper = mapper;
        // _httpContextAccessor = httpContextAccessor;
    }

    public async Task UpdateLastActivityAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            user.LastActivity = DateTime.UtcNow;
            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded) { } // Log
        } // Log
    }

    // public async Task<bool> IsUserAuthorized(string loggedInUserId)
    // {
    //     bool result = false;
    //     try
    //     {
    //         string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //         result = !userId.IsNullOrEmpty();
    //     }
    //     catch (Exception e)
    //     {
    //         // await _userService.UpdateLastActivityAsync(loggedInUserId);
    //         result = false;
    //     }
    //
    //     return result;
    // }

    public async Task<IServiceResult<List<ApplicationUserDto>>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDtos = _mapper.Map<List<ApplicationUserDto>>(users);

        return ServiceResult<List<ApplicationUserDto>>.Success(userDtos);
    }
}