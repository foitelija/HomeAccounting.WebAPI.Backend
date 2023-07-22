using HomeAccounting.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HomeAccounting.API.HealthcheckServices
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly AccountingDbContext _context;

        public DbHealthCheck(AccountingDbContext context)
        {
            _context = context;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _context.Database.OpenConnection();
                _context.Database.CloseConnection();
                return Task.FromResult(
                        HealthCheckResult.Healthy("База данных поднята и работает."));
            }
            catch (Exception)
            {
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus, "База данных отключена"));
            }
        }
    }
}
