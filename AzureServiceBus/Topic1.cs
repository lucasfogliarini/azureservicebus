using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AzureServiceBus
{
    public class Topic1
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<Topic1> _logger;

        public Topic1(ILogger<Topic1> log, BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _logger = log;
        }

        [FunctionName("Topic1")]
        public void Run([ServiceBusTrigger("topic1", "sub1", Connection = "AzureServiceBus")]
                        Account account,
                        Int32 deliveryCount,
                        DateTime enqueuedTimeUtc,
                        string messageId)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {account},");
            _logger.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
            _logger.LogInformation($"DeliveryCount={deliveryCount}");
            _logger.LogInformation($"MessageId={messageId}");

            var container1 = _blobServiceClient.GetBlobContainerClient("container1");
            var blob = container1.GetBlobClient("athenas.png");
            var blobDowloadedContent = blob.DownloadContent();
            var blobContent = blobDowloadedContent.Value.Content;
            var blobBytes = blobContent.ToArray();
            var blobStream = blobContent.ToStream();
        }
    }
}
