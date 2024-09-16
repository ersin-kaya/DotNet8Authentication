using CustomIdentityAuth.Dtos.Concretes;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface IUserService
{
    Task<IServiceResult<List<ApplicationUserDto>>> GetUsersAsync();
}