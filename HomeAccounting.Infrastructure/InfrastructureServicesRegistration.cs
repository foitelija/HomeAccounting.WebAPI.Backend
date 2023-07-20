using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAccounting.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyService, CurrencyService>();
            return services;
        }
    }
}
