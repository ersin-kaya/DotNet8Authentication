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
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName
        };
    }

    public static CreatedUserDto<string> MapToCreatedUserDto(this ApplicationUser user,  IList<string> roles)
    {
        return new CreatedUserDto<string>
        {
            Email = user.Email,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            LastActivity = user.LastActivity
        };
    }
}