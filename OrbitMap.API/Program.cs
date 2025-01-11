using System.Text.Json.Serialization;
using Hangfire;
using OrbitMap.API.Constants;
using OrbitMap.API.Extensions;
using OrbitMap.API.Helper;
using OrbitMap.API.Logger;
using OrbitMap.API.Middlewares;
using OrbitMap.API.Services.Implement;
using OrbitMap.API.SignalR;
using OrbitMap.Domain;
using OrbitMap.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(SeriLogger.Configure);
Log.Information("Starting OrbitMap API up");
try
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: CorsConstant.PolicyName,
            policy => { policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader().AllowAnyMethod().AllowCredentials(); });
    });
    builder.Services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        // x.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });
    builder.Host.AddAppConfigurations();
    builder.Services.AddServices();
    builder.Services.AddRepositoryServices();
    builder.Services.AddDomainServices(builder.Configuration);
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddRedis(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSignalR(options =>
    {
        options.EnableDetailedErrors = true;
    });
    builder.Services.AddSingleton<PresenceTracker>();
    builder.Services.AddJwtValidation();
    builder.Services.AddConfigSwagger();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHttpsRedirection(
        options => { options.HttpsPort = 5001; });
    var app = builder.Build();

    if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrbitMap API V1");
            c.InjectStylesheet("/assets/css/kkk.css");
        });
    }
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseCors(CorsConstant.PolicyName);
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapControllers();
    app.UseHangfireDashboard();
    app.UseHangfireServer();
    app.MapHub<PresenceHub>("hubs/presence");
    app.MapHub<MessageHub>("hubs/message");
    // RecurringJob.AddOrUpdate<StoryCleanupService>(
    //     "remove-expired-stories",
    //     job => job.RemoveExpiredStories(),
    //     Cron.Hourly
    // );
    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, $"Unhandled: {ex.Message}");
}
finally
{
    Log.Information("Shut down OrbitMap API complete");
    Log.CloseAndFlush();
}