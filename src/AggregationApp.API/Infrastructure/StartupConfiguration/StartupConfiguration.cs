using AggregationApp.Application;
using AggregationApp.Infrastructure;
using AggregationApp.Persistence;
using Microsoft.OpenApi.Models;

namespace AggregationApp.API.Infrastructure.StartupConfiguration
{
    public static class StartupConfiguration
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;
            // Add services to the container.

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(sw =>
            {
                sw.SwaggerDoc("v1", new OpenApiInfo { Title = "Aggregation.Api", Version = "v1" });
            });

            services.AddPersistence(configuration);
            services.AddApplication(configuration);
            services.AddInfrastructure(configuration);

            return builder;
        }
    }
}
