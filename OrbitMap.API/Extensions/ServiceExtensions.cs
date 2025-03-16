using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrbitMap.API.Services.Implement;
using OrbitMap.API.Services.Interface;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OrbitMap.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFriendshipService, FriendshipService>();
        services.AddScoped<IOneSignalService, OneSignalService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<IRedisService, RedisService>();
        services.AddScoped<ISmsService, SmsService>();
        services.AddScoped<ILastMessageChatService, LastMessageChatService>();
        services.AddScoped<IUploadService, UploadService>();
        services.AddScoped<IStoryService, StoryService>();
        services.AddScoped<ICraftMyPdfService, CraftMyPdfService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<IBusinessService, BusinessService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IFoodyService, FoodyService>();
        services.AddScoped<IOverseaService, OverseaService>();
        services.AddScoped<IVietMapService, VietMapService>();
        services.AddScoped<ITravelPlanService, TravelPlanService>();
        services.AddScoped<IDashboardService, DashboardService>();
        return services;
    }

    public static IServiceCollection AddJwtValidation(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = "OrbitMap",
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("taolabocuachungmaychungmaylaconcuatao"))
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        context.Token = accessToken;
                    return Task.CompletedTask;
                }
            };
        });
        return services;
    }

    public static IServiceCollection AddConfigSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API OrbitMap V1", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            options.MapType<TimeOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "time",
                Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42.0000000\"")
            });
        });
        return services;
    }
}