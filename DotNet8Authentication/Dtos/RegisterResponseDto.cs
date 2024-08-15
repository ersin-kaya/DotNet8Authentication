using DotNet8Authentication.Models;

namespace DotNet8Authentication.Dtos;

public class RegisterResponseDto
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? ErrorMessages { get; set; }
    public CreatedUserDto<string>? CreatedUser { get; set; }
    public UserToken? UserToken { get; set; }
}