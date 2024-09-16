using Microsoft.AspNetCore.Identity;

namespace CustomIdentityAuth.Data.DataSeeds;

public static class IdentityUserRoleSeed
{
    public static List<IdentityUserRole<string>> GetUserRoles()
    {
        return new List<IdentityUserRole<string>>
        {
            new()
            {
                UserId = ApplicationUserSeed.AdminId,
                RoleId = IdentityRoleSeed.AdminRoleId
            },
            new()
            {
                UserId = ApplicationUserSeed.UserId,
                RoleId = IdentityRoleSeed.UserRoleId
            }
        };
    }
}
