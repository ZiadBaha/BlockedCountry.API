using BlockedCountry.API.Extensions;
using BlockedCountry.API.Filters; 

namespace BlockedCountry.API.Setup
{
    public static class ServiceConfigurator
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>(); 
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerDocumentation();
            services.AddApplicationServices(configuration);
            services.AddFluentValidationServices();
        }
    }
}
