using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using AdvertisementService.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace AdvertisementService.Infrastructure.Clients
{
    public class IdentityServiceClient : IIdentityServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _identityServiceBaseUrl;
        private readonly string _jobServiceBaseUrl;
        private readonly ILogger<IdentityServiceClient> _logger;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy;

        public IdentityServiceClient(
            IHttpClientFactory httpClientFactory,
            string identityServiceBaseUrl,
            string jobServiceBaseUrl,
            ILogger<IdentityServiceClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _identityServiceBaseUrl = identityServiceBaseUrl.TrimEnd('/');
            _jobServiceBaseUrl = jobServiceBaseUrl.TrimEnd('/');
            _logger = logger;

            // Retry policy: 3 retries with exponential backoff
            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode && r.StatusCode != HttpStatusCode.NotFound)
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (outcome, timespan, retryCount, context) =>
                    {
                        _logger.LogWarning(
                            "Retry {RetryCount} after {Delay}ms for {Operation}",
                            retryCount, timespan.TotalMilliseconds, context.OperationKey);
                    });

            // Circuit breaker: opens after 5 consecutive failures, stays open for 30 seconds
            _circuitBreakerPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode && r.StatusCode != HttpStatusCode.NotFound)
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    onBreak: (result, duration) =>
                    {
                        _logger.LogError(
                            "Circuit breaker opened for {Duration}s due to: {Reason}",
                            duration.TotalSeconds, result?.Result?.StatusCode);
                    },
                    onReset: () =>
                    {
                        _logger.LogInformation("Circuit breaker reset");
                    });
        }

        public async Task<string?> GetCompanyOwnerUserIdAsync(int companyId, CancellationToken ct = default)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_jobServiceBaseUrl);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var context = new Context { ["OperationKey"] = "GetCompanyOwner" };

            var response = await _circuitBreakerPolicy
                .WrapAsync(_retryPolicy)
                .ExecuteAsync(async (ctx) =>
                {
                    return await httpClient.GetAsync($"{_jobServiceBaseUrl}/api/companies/{companyId}/owner", ct);
                }, context);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CompanyOwnerResponse>(cancellationToken: ct);
            return result?.UserId;
        }

        public async Task<bool> UserExistsAsync(string userId, CancellationToken ct = default)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_identityServiceBaseUrl);
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            var context = new Context { ["OperationKey"] = "UserExists" };

            var response = await _circuitBreakerPolicy
                .WrapAsync(_retryPolicy)
                .ExecuteAsync(async (ctx) =>
                {
                    return await httpClient.GetAsync($"{_identityServiceBaseUrl}/api/users/{userId}/exists", ct);
                }, context);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        private class CompanyOwnerResponse
        {
            public string UserId { get; set; } = string.Empty;
        }
    }
}

