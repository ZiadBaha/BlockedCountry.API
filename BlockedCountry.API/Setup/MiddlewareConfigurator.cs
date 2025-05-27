using BlockedCountry.API.Extensions;

namespace BlockedCountry.API.Setup
{
    public static class MiddlewareConfigurator
    {
        public static void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwaggerDocumentation();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCustomMiddlewares();

            app.MapControllers();
        }
    }
}
