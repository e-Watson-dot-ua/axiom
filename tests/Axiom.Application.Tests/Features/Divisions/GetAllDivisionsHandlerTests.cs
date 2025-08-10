using System.Linq.Expressions;
using Axiom.Application.Contracts.Persistence;
using Axiom.Application.Features.Divisions.Queries;
using Axiom.Domain.Entities.Divisions;
using Moq;

namespace Axiom.Application.Tests.Features.Divisions;

public class GetAllDivisionsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllDivisions()
    {
        // Arrange
        var divisions = new List<DivisionsItem>
        {
            new(Guid.NewGuid(), null, "CODE1", "Short1", "Name1", 1, false),
            new(Guid.NewGuid(), null, "CODE2", "Short2", "Name2", 2, true)
        };

        var repoMock = new Mock<IRepository<Division>>();
        repoMock.Setup(r => r.ListAsync(
                null,
                It.IsAny<Expression<Func<Division, DivisionsItem>>>(),
                0, int.MaxValue, true, It.IsAny<CancellationToken>()))
            .ReturnsAsync(divisions);

        var uowMock = new Mock<IUnitOfWork>();
        uowMock.Setup(u => u.GetRepository<Division>()).Returns(repoMock.Object);

        var handler = new GetAllDivisionsHandler(uowMock.Object);

        // Act
        var result = await handler.Handle(new GetAllDivisions(), CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, d => d.Code == "CODE1");
        Assert.Contains(result, d => d.Code == "CODE2");
    }
}