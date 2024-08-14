using System.ComponentModel.DataAnnotations;

namespace DotNet8Authentication.Models;

public class RegisterModel
{
    [Required]
    [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    
    [Required]
    [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
    public string FullName { get; set; }
}