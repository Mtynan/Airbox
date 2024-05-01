using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILocalMemoryRepository, LocalMemoryRepository>();

            return services;
        }
    }
}
