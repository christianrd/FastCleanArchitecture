namespace FastCleanArchitecture.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    protected BaseAuditableEntity(Guid id) : base(id)
    {
    }

    protected BaseAuditableEntity() : base()
    {
    }

    public DateTimeOffset CreatedAtUtc { get; protected set; } = DateTimeOffset.UtcNow;
    public string? CreatedBy { get; protected set; }
    public DateTimeOffset ModifiedAtUtc { get; protected set; }
    public string? ModifiedBy { get; protected set; }
}
