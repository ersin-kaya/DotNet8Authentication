using AutoMapper;
using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;
using CustomIdentityAuth.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomIdentityAuth.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IServiceResult<List<ApplicationUserDto>>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDtos = _mapper.Map<List<ApplicationUserDto>>(users);
        
        return ServiceResult<List<ApplicationUserDto>>.Success(userDtos);
    }
}