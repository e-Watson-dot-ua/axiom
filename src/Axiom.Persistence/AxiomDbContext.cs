using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Axiom.Persistence;

public class AxiomDbContext(DbContextOptions<AxiomDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}