using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace AzureServiceBus
{
    public class Topic1
    {
        private readonly ILogger<Topic1> _logger;

        public Topic1(ILogger<Topic1> log)
        {
            _logger = log;
        }

        [FunctionName("Topic1")]
        public void Run([ServiceBusTrigger("topic1", "local", Connection = "CognaMessages")]
                        Account account,
                        Int32 deliveryCount,
                        DateTime enqueuedTimeUtc,
                        string messageId)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {account},");
            _logger.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
            _logger.LogInformation($"DeliveryCount={deliveryCount}");
            _logger.LogInformation($"MessageId={messageId}");
        }

        public class Account
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
