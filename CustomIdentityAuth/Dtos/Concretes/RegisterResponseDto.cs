using CustomIdentityAuth.Dtos.Abstracts;

namespace CustomIdentityAuth.Dtos.Concretes;

public class RegisterResponseDto : IResponseDto
{
    public CreatedUserDto<string>? CreatedUser { get; set; }
    //public AuthTokenDto? AuthTokenDto { get; set; }
}