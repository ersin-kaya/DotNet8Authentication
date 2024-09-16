using System.ComponentModel.DataAnnotations;
using CustomIdentityAuth.Constants;
using CustomIdentityAuth.Dtos.Abstracts;

namespace CustomIdentityAuth.Dtos.Concretes;

public class RegisterRequestDto : IRequestDto
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
    [StringLength(ValidationConstants.FirstNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.FirstNameMinLength)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(ValidationConstants.LastNameMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.LastNameMinLength)]
    public string LastName { get; set; }
}