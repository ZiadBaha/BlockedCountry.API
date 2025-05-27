using BlockedCountry.API.Extensions;
using Microsoft.Extensions.Configuration;

namespace BlockedCountry.API.Setup
{
    public static class ServiceConfigurator
    {
        public static void Configure(IServiceCollection services , IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerDocumentation();
            services.AddApplicationServices(configuration);
        }
    }
}
