using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi.Services
{
    using Microsoft.Extensions.Logging;

    public interface ILoggingService
    {
        void Log(string message);
    }

    public class UppercaseLoggingService : ILoggingService
    {
        private readonly ILogger<UppercaseLoggingService> logger;
        private readonly Guid instanceId;

        public UppercaseLoggingService(ILogger<UppercaseLoggingService> logger)
        {
            this.logger = logger;
            this.instanceId = Guid.NewGuid();
        }

        public void Log(string message)
        {
            var upper = message.ToUpper();
            logger.LogInformation($"{this.instanceId} - {upper}");
        }
    }

    public class LowercaseLoggingService : ILoggingService
    {
        private readonly ILogger<LowercaseLoggingService> logger;
        private readonly Guid instanceId;

        public LowercaseLoggingService(ILogger<LowercaseLoggingService> logger)
        {
            this.logger = logger;
            logger.LogInformation("Ctor LowercaseLoggingService called");
            this.instanceId = Guid.NewGuid();
        }

        public void Log(string message)
        {
            var upper = message.ToLower();
            logger.LogInformation($"{this.instanceId} - {upper}");
        }
    }

    public class MailService
    {
        private readonly ILoggingService service;

        public MailService(ILoggingService service)
        {
            this.service = service;
        }

        public void SendMail(string message)
        {
            this.service.Log($"mail sent. message: {message}");
        }
    }
}
