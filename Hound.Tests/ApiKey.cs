using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Hound.Tests
{
    public class TestApiKey
        : ApiKey
    {
        public override string GetApiKey()
        {
            string configPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: true)
                .Build();

            return configuration["DatadogApiKey"];
        }
    }
}