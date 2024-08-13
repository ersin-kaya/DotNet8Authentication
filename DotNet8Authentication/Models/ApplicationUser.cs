using Microsoft.AspNetCore.Identity;

namespace DotNet8Authentication.Models;

public class ApplicationUser : IdentityUser
{
    public string? Address { get; set; }
}