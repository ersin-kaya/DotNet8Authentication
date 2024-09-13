using CustomIdentityAuth.Dtos.Abstracts;

namespace CustomIdentityAuth.Dtos.Concretes;

public class ApplicationUserDto : IDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}