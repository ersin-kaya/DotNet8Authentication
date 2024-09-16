using System.ComponentModel.DataAnnotations;
using CustomIdentityAuth.Constants;
using CustomIdentityAuth.Dtos.Abstracts;

namespace CustomIdentityAuth.Dtos.Concretes;

public class LoginRequestDto : IRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(ValidationConstants.PasswordMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.PasswordMinLength)]
    public string Password { get; set; }
}