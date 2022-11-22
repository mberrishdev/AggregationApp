using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AggregationApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
