using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;

namespace OrbitMap.Repository;

public static class ConfigureServices
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork<OrbitMapContext>, UnitOfWork<OrbitMapContext>>();
        return services;
    }
}