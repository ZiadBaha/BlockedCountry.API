using BlockedCountry.Application.Configurations;
using Microsoft.Extensions.Configuration;

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

            return services;
        }

    }
}
