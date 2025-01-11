using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrbitMap.Domain.Configurations;
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
            config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnectionString"),
                new SqlServerStorageOptions
                {
                    EnableHeavyMigrations = true,
                    DisableGlobalLocks = true
                });
        });
        services.AddHangfireServer();
        services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
        services.Configure<CraftMyPdfSettings>(configuration.GetSection("CraftMyPdf"));
        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
        return services;
    }
}