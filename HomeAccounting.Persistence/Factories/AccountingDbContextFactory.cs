using HomeAccounting.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HomeAccounting.Persistence.Factories
{
    public class AccountingDbContextFactory : IDesignTimeDbContextFactory<AccountingDbContext>
    {
        public AccountingDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AccountingDbContext>();
            var connectionString = configuration.GetConnectionString("homeAccConnectionString");

            builder.UseSqlServer(connectionString);

            return new AccountingDbContext(builder.Options);
        }
    }
}
