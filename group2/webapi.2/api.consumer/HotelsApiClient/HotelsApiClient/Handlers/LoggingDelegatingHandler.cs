using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HotelsApiClient.Handlers
{
    using Microsoft.Extensions.Logging;

    public class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingDelegatingHandler> logger;

        public LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
        {
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"Request: {request}");

            try
            {
                // base.SendAsync calls the inner handler
                var response = await base.SendAsync(request, cancellationToken);
                this.logger.LogInformation($"Response: {response}");
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Failed to get response: {ex}");
                throw;
            }
        }
    }
}
