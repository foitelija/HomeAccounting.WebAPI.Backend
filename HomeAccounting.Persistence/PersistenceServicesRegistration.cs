using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Persistence.Context;
using HomeAccounting.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAccounting.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccountingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("homeAccConnectionString"));
            });

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
