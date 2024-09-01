using CustomIdentityAuth.Models;

namespace CustomIdentityAuth.Dtos;

public class LoginResponseDto
{
    public UserToken? UserToken { get; set; }
}