using DotNet8Authentication.Constants;
using DotNet8Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNet8Authentication.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private static readonly PasswordHasher<ApplicationUser> _passwordHasher = new();
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    private List<ApplicationUser> applicationUsers = new()
    {
        new ApplicationUser
        {
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            FullName = "Admin",
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PasswordHash = _passwordHasher.HashPassword(null, "String1234.")
        },
        new ApplicationUser
        {
            UserName = "test_user",
            NormalizedUserName = "TEST_USER",
            Email = "test_user@example.com",
            NormalizedEmail = "TEST_USER@EXAMPLE.COM",
            FullName = "Test User",
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PasswordHash = _passwordHasher.HashPassword(null, "String1234.")
        }
    };

    private List<IdentityRole> roles = new()
    {
        new IdentityRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = RoleConstants.Admin,
            NormalizedName = RoleConstants.Admin.ToUpper()
        },
        new IdentityRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = RoleConstants.User,
            NormalizedName = RoleConstants.User.ToUpper()
        }
    };

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>().HasData(applicationUsers);
        builder.Entity<IdentityRole>().HasData(roles);
    }
}