using CustomIdentityAuth.Dtos.Abstracts;

namespace CustomIdentityAuth.Dtos.Concretes;

public class CreatedUserDto<TId> : IDto
{
    public TId Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime LastActivity { get; set; }
    public IList<string> Roles { get; set; }
}