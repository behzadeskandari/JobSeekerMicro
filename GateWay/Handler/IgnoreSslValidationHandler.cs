namespace GateWay.Handler
{
    public class IgnoreSslValidationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Bypass SSL certificate validation errors
            if (request.RequestUri?.Host.Contains("localhost") == true)
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                var client = new HttpClient(handler);
                return await client.SendAsync(request, cancellationToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
