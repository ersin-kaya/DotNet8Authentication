using CustomIdentityAuth.Dtos;
using CustomIdentityAuth.Models;

namespace CustomIdentityAuth.Mappers;

public static class UserMapper
{
    public static ApplicationUser MapToApplicationUserForRegister(this RegisterModel model)
    {
        return new ApplicationUser(model.UserName)
        {
            Email = model.Email,
            FullName = model.FullName
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