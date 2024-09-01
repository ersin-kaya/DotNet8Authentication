using System.ComponentModel.DataAnnotations;
using CustomIdentityAuth.Constants;

namespace CustomIdentityAuth.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(ValidationConstants.PasswordMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.PasswordMinLength)]
    public string Password { get; set; }
}