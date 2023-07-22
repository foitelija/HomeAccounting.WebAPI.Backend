using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;

namespace HomeAccounting.API.HealthcheckServices
{
    public class CurrencyApiHealth : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IUrlHelper _urlHelper;

        public CurrencyApiHealth(IHttpClientFactory httpClientFactory/*, IUrlHelper urlHelper*/)
        {
            _httpClientFactory = httpClientFactory;
            //_urlHelper = urlHelper;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync("https://api.nbrb.by/exrates/rates/USD?parammode=2");
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(new HealthCheckResult(
                      status: HealthStatus.Healthy,
                      description: "The API is up and running."));
                }
                return await Task.FromResult(new HealthCheckResult(
                  status: HealthStatus.Unhealthy,
                  description: "The API is down."));
            }
        }
    }
}
