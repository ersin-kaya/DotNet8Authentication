using CustomIdentityAuth.Models;

namespace CustomIdentityAuth.Dtos;

public class LoginResponseDto
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? ErrorMessages { get; set; } = [];
    public UserToken? UserToken { get; set; }
}