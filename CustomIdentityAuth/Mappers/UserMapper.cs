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
}