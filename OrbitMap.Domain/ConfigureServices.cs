using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrbitMap.Domain.Persistent;
using StackExchange.Redis;

namespace OrbitMap.Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrbitMapContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"),
                builder => builder.MigrationsAssembly(typeof(OrbitMapContext).Assembly.FullName));
        });
        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnectionString"));
        });
        services.AddHangfireServer();
        return services;
    }
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
        return services;
    }
}