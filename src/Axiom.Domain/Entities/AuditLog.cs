using Axiom.Domain.Common;

namespace Axiom.Domain.Entities;

public class AuditLog : Entity
{
    public string TableName { get; set; } = string.Empty;
    public Guid RecordId { get; set; }

    public DateTime Timestamp { get; set; }
    public string Action { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string? Details { get; set; }
}