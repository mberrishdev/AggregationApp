using AggregationApp.Application.Contracts.Infrastructure.ElectricityMeteringService;
using AggregationApp.Infrastructure.MeteringPlant;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AggregationApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IElectricityMeteringService, ElectricityMeteringService>();
            return services;
        }
    }
}
