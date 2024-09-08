using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Results;

namespace CustomIdentityAuth.Services;

public interface IUserService
{
    Task<IServiceResult<List<ApplicationUserDto>>> GetUsersAsync();
}