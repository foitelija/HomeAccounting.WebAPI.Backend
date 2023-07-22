using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;

namespace HomeAccounting.API.HealthcheckServices
{
    public class PurchaseHealth : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PurchaseHealth(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7289/api/purchases");
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(new HealthCheckResult(
                      status: HealthStatus.Healthy,
                      description: "API сервис поднят и работает."));
                }
                return await Task.FromResult(new HealthCheckResult(
                  status: HealthStatus.Unhealthy,
                  description: "API отключен."));
            }
        }
    }
}
