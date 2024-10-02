using CustomIdentityAuth.Dtos.Concretes;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface IUserService
{
    // Task<bool> IsUserAuthorized(string loggedInUserId);
    Task UpdateLastActivityAsync(string userId);
    Task<IServiceResult<List<ApplicationUserDto>>> GetUsersAsync();
}