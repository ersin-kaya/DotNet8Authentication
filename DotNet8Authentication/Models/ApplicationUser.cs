using Microsoft.AspNetCore.Identity;

namespace DotNet8Authentication.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    
    public ApplicationUser()
    {
    }

    public ApplicationUser(string userName) : base(userName)
    {
    }
}