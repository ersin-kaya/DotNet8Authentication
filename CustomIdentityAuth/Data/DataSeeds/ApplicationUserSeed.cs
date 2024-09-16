using CustomIdentityAuth.Entities.Concretes;
using Microsoft.AspNetCore.Identity;

namespace CustomIdentityAuth.Data.DataSeeds;

public static class ApplicationUserSeed
{
    private static readonly PasswordHasher<ApplicationUser> _passwordHasher = new();

    public static readonly string AdminId = "b2f5c3e7-2a6c-4d7a-8b47-fb2b0a2d9f8c";
    public static readonly string UserId = "8a6c17f4-3baf-4e59-b6f8-1b0e7d6d2f4b";

    public static List<ApplicationUser> GetApplicationUsers()
    {
        return new List<ApplicationUser>
        {
            new()
            {
                Id = AdminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                FirstName = "Admin",
                LastName = string.Empty,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = _passwordHasher.HashPassword(null, "String1234.")
            },
            new()
            {
                Id = UserId,
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@example.com",
                NormalizedEmail = "USER@EXAMPLE.COM",
                FirstName = "User",
                LastName = string.Empty,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = _passwordHasher.HashPassword(null, "String1234.")
            }
        };
    }
}
