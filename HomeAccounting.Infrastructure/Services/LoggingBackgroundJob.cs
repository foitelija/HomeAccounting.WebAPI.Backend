using Microsoft.Extensions.Logging;
using Quartz;
using System.Diagnostics;

namespace HomeAccounting.Infrastructure.Services
{
    [DisallowConcurrentExecution]
    public class LoggingBackgroundJob : IJob
    {
        private readonly ILogger _logger;

        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"{DateTime.UtcNow}");
            
            return Task.CompletedTask;
        }
    }
}
