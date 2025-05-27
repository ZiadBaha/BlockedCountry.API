using BlockedCountry.API.Setup;

namespace BlockedCountry.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ServiceConfigurator.Configure(builder.Services, builder.Configuration);

            var app = builder.Build();

            MiddlewareConfigurator.Configure(app, builder.Environment);

            app.Run();
        }
    }
}
