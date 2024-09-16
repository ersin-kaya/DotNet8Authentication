using CustomIdentityAuth.Data.DataSeeds;
using CustomIdentityAuth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomIdentityAuth.Data;

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