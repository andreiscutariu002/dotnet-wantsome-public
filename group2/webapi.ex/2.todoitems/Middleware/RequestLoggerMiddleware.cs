using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Services;

    public class RequestLoggerMiddleware
    {
        private readonly ILoggingService logger;
        private readonly RequestDelegate next;

        public RequestLoggerMiddleware(RequestDelegate next, ILoggingService service)
        {
            this.next = next;
            this.logger = service;
        }

        public async Task Invoke(HttpContext context)
        {
            this.logger.Log("Handling request: " + context.Request.Path);
            
            await this.next.Invoke(context);
            
            this.logger.Log("Finished handling request.");
        }
    }
}
