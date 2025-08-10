namespace Axiom.Application.Features.Divisions.Queries;

public record GetAllDivisions;
public record DivisionsItem(Guid Id, Guid? ParentId, string Code,
    string ShortName, string Name, int SortOrder, bool IsInternal);