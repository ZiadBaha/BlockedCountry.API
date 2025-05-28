using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BlockedCountry.API.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<BlockedCountry.Application.Validators.BlockedCountryValidator>();

            return services;
        }
    }
}
