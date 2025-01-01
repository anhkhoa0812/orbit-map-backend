using System.Text.Json.Serialization;
using OrbitMap.API.Constants;
using OrbitMap.API.Extensions;
using OrbitMap.API.Logger;
using OrbitMap.API.Middlewares;
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
            policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
    });
    builder.Services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        // x.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });
    
    builder.Host.AddAppConfigurations();
    builder.Services.AddRepositoryServices();
    builder.Services.AddDomainServices(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddConfigSwagger();
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseCors(CorsConstant.PolicyName);
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseHttpsRedirection();
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