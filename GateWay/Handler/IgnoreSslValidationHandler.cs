namespace GateWay.Handler
{
    public class IgnoreSslValidationHandler : DelegatingHandler
    {
        public IgnoreSslValidationHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Only bypass SSL for localhost
            if (request.RequestUri?.Host.Contains("localhost") == true)
            {
                // Get the current handler (usually SocketsHttpHandler)
                if (InnerHandler is HttpClientHandler clientHandler)
                {
                    clientHandler.ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
