using CustomIdentityAuth.Constants;
using Microsoft.AspNetCore.Identity;

namespace CustomIdentityAuth.Data.DataSeeds;

public static class IdentityRoleSeed
{
    public static readonly string AdminRoleId = "c8a9f7f2-4e6a-4c6d-b6b4-d3b2e2e6c7b2";
    public static readonly string UserRoleId = "6b7d7e8c-5b7a-4c5d-b9f6-f2e9d7c4b1d3";

    public static List<IdentityRole> GetIdentityRoles()
    {
        return new List<IdentityRole>
        {
            new()
            {
                Id = AdminRoleId,
                Name = RoleConstants.Admin,
                NormalizedName = RoleConstants.Admin.ToUpper()
            },
            new()
            {
                Id = UserRoleId,
                Name = RoleConstants.User,
                NormalizedName = RoleConstants.User.ToUpper()
            }
        };
    }
}
