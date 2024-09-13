using CustomIdentityAuth.Dtos.Abstracts;

namespace CustomIdentityAuth.Dtos.Concretes;

public class LoginResponseDto : IResponseDto
{
    public AuthTokenDto? AuthTokenDto { get; set; }
}