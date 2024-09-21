using Hangfire;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Hangfire.Redis.StackExchange;

namespace VideoConverter;

public static class RegisterServices
{
    public static void Register(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });

        services.Configure<HangFire>(configuration.GetSection(HangFire.SectionName));

        services.AddHangfire((sp, options) =>
        {
            var hangFireSettings = sp.GetRequiredService<IOptions<HangFire>>().Value;
            options.UseSqlServerStorage(hangFireSettings.ConnectionString)
                   .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                   .UseDefaultTypeSerializer();

            //options.UseRedisStorage(hangFireSettings.ConnectionString);
            //.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            //.UseDefaultTypeSerializer();
        }
        );

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = 10;
        });
    }
}
