namespace HotelsApiClient.Handlers
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class RetryPolicyDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<RetryPolicyDelegatingHandler> logger;
        private readonly int maximumAmountOfRetries;

        public RetryPolicyDelegatingHandler(ILogger<RetryPolicyDelegatingHandler> logger) : this(10)
        {
            this.logger = logger;
        }

        public RetryPolicyDelegatingHandler(int maximumAmountOfRetries)
        {
            this.maximumAmountOfRetries = maximumAmountOfRetries;
        }

        public RetryPolicyDelegatingHandler(HttpMessageHandler innerHandler,
            int maximumAmountOfRetries)
            : base(innerHandler)
        {
            this.maximumAmountOfRetries = maximumAmountOfRetries;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            for (var i = 0; i < this.maximumAmountOfRetries; i++)
            {
                response = await base.SendAsync(request, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    this.logger.LogInformation($"Retry {i}...");
                }
            }

            return response;
        }
    }
}
