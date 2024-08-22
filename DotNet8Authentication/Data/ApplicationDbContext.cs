using DotNet8Authentication.Constants;
using DotNet8Authentication.Data.DataSeeds;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>().HasData(ApplicationUserSeed.GetApplicationUsers());
        builder.Entity<IdentityRole>().HasData(IdentityRoleSeed.GetIdentityRoles());
        builder.Entity<IdentityUserRole<string>>().HasData(IdentityUserRoleSeed.GetUserRoles());
    }
}