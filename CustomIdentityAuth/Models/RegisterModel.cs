using System.ComponentModel.DataAnnotations;
using CustomIdentityAuth.Constants;

namespace CustomIdentityAuth.Models;

public class RegisterModel
{
    [Required]
    [StringLength(ValidationConstants.UserNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.UserNameMinLength)]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(ValidationConstants.PasswordMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.PasswordMinLength)]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    
    [Required]
    [StringLength(ValidationConstants.FullNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.FullNameMinLength)]
    public string FullName { get; set; }
}