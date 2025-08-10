using Axiom.Application.Contracts.Persistence;
using Axiom.Domain.Entities.Divisions;

namespace Axiom.Application.Features.Divisions.Queries;

public class GetAllDivisionsHandler(IUnitOfWork unitOfWork)
{
    public async Task<IReadOnlyList<DivisionsItem>> Handle(
        GetAllDivisions query, CancellationToken ct)
    {
        var repository = unitOfWork.GetRepository<Division>();
        return await repository.ListAsync(
            predicate: null,
            selector: d => new DivisionsItem(
                d.Id, d.ParentId, d.Code, d.ShortName, d.Name,
                d.SortOrder, d.IsInternal
            ),
            skip: 0,
            take: int.MaxValue,
            asNoTracking: true,
            ct: ct
        );
    }
}