namespace CustomIdentityAuth.Dtos;

public class RegisterResponseDto
{
    public CreatedUserDto<string>? CreatedUser { get; set; }
    //public UserToken? UserToken { get; set; }
}