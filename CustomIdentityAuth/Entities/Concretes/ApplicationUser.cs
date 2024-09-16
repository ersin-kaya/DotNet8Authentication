using Microsoft.AspNetCore.Identity;

namespace CustomIdentityAuth.Entities.Concretes;

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