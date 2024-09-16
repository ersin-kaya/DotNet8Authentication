using CustomIdentityAuth.Dtos.Concretes;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface IUserService
{
    Task UpdateLastActivityAsync(string email);
    Task<IServiceResult<List<ApplicationUserDto>>> GetUsersAsync();
}