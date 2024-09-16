using Microsoft.AspNetCore.Identity;

namespace CustomIdentityAuth.Entities.Concretes;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;
    
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsActive { get; set; } = true;

    public ApplicationUser()
    {
        CreatedDate = DateTime.UtcNow;
    }

    public ApplicationUser(string userName) : base(userName)
    {
        CreatedDate = DateTime.UtcNow;
    }
}