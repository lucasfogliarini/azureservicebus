using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(AzureServiceBus.Startup))]
namespace AzureServiceBus
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<AccountService>();

            var connectionString = Environment.GetEnvironmentVariable("StorageAccount");
            builder.Services.AddSingleton(new BlobServiceClient(connectionString));
        }
    }
}