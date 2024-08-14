using DotNet8Authentication.Dtos;
using DotNet8Authentication.Models;

namespace DotNet8Authentication.Services;

public interface IAccountService
{
    Task<RegisterResponseDto> Register(RegisterModel model);
}