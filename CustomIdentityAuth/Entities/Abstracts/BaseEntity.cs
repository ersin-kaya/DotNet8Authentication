namespace CustomIdentityAuth.Entities.Abstracts;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsActive { get; set; } = true;
    
    protected BaseEntity()
    {
        CreatedDate = DateTime.UtcNow;
    }
}