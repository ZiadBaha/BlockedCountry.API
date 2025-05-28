using BlockedCountry.Application.Configurations;
using BlockedCountry.Application.Services;
using BlockedCountry.Infrastructure.Repositories;
using BlockedCountry.Infrastructure.Services.Background;
using BlockedCountry.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using BlockedCountry.Application.Interfaces;

namespace BlockedCountry.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddOptions<IpGeolocationSettings>()
                    .Bind(configuration.GetSection("IpGeolocation"))
                    .ValidateDataAnnotations();

            // Repositories
            services.AddScoped<IBlockedCountryRepository, BlockedCountryRepository>();
            services.AddScoped<IBlockedAttemptLogRepository, BlockedAttemptLogRepository>();
            services.AddSingleton<ITemporalBlockedCountryRepository, TemporalBlockedCountryRepository>();
            services.AddScoped<IBlockedAttemptLogRepository, BlockedAttemptLogRepository>();
            services.AddScoped(typeof(IInMemoryRepository<,>), typeof(InMemoryRepository<,>));

            // Services
            services.AddHttpClient<IIpGeolocationService, IpGeolocationService>();
            services.AddScoped<IBlockedCountryService, BlockedCountryService>();
            services.AddScoped<ILogService, LogService>();




            // Background service
            services.AddHostedService<TemporalBlockCleaner>();

            return services;
        }

    }
}
