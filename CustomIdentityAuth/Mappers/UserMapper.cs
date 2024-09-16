using CustomIdentityAuth.Dtos.Concretes;
using CustomIdentityAuth.Entities.Concretes;

namespace CustomIdentityAuth.Mappers;

public static class UserMapper
{
    public static ApplicationUser MapToApplicationUserForRegister(this RegisterRequestDto requestDto)
    {
        return new ApplicationUser(requestDto.UserName)
        {
            Email = requestDto.Email,
            FullName = requestDto.FullName
        };
    }

    public static CreatedUserDto<string> MapToCreatedUserDto(this ApplicationUser user,  IList<string> roles)
    {
        return new CreatedUserDto<string>
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            FullName = user.FullName,
            Roles = roles
        };
    }
}