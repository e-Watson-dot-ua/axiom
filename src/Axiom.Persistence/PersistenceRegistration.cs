using Axiom.Application.Contracts.Persistence;
using Axiom.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Axiom.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AxiomDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("AxiomDbConnection");
            options.UseSqlServer(connectionString);
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork>(provider =>
        {
            var context = provider.GetRequiredService<AxiomDbContext>();
            return new UnitOfWork(context);
        });

        return services;
    }
}