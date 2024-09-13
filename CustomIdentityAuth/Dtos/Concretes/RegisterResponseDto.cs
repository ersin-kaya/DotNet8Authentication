namespace CustomIdentityAuth.Dtos.Concretes;

public class RegisterResponseDto
{
    public CreatedUserDto<string>? CreatedUser { get; set; }
    //public AuthTokenDto? AuthTokenDto { get; set; }
}