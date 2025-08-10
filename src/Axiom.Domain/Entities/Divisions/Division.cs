using Axiom.Domain.Common;

namespace Axiom.Domain.Entities.Divisions;

public class Division : Entity
{
    public Guid? ParentId { get; set; }

    public string Code { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public int SortOrder { get; set; }
    public bool IsInternal { get; set; }
}