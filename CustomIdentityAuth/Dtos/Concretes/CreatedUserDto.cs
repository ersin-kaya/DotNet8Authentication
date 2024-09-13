namespace CustomIdentityAuth.Dtos.Concretes;

public class CreatedUserDto<TId>
{
    public TId Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public IList<string> Roles { get; set; }
}