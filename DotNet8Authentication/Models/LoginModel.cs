using System.ComponentModel.DataAnnotations;

namespace DotNet8Authentication.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string Password { get; set; }
}