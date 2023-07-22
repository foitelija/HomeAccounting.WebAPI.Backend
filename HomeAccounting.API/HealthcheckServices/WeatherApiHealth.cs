using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Net.Http;

namespace HomeAccounting.API.HealthcheckServices
{
    public class WeatherApiHealth : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUrlHelper _urlHelper;

        public WeatherApiHealth(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;


        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
