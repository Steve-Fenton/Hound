using Microsoft.Extensions.Configuration;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Hound.Tests
{
    public class DogTests
    {
        private readonly string _testApiKey;

        public DogTests()
        {
            string configPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: true)
                .Build();

            _testApiKey = configuration["DatadogApiKey"];
        }

        [Fact]
        public async Task RaiseErrorShouldBeSuccessful()
        {
            IEventDestination target = new DogEvents(_testApiKey);
            HoundEvent data = GetTestEvent();

            HoundResult eventResponse = await target.Publish(data);

            eventResponse.IsSuccess.ShouldBe(true);
        }

        [Fact]
        public async Task RaiseMetricShouldBeSuccessful()
        {
            IMetricDestination target = new DogMetrics(_testApiKey);
            HoundMetricCollection data = GetTestMetrics();

            HoundResult eventResponse = await target.RaiseMetric(data);

            eventResponse.IsSuccess.ShouldBe(true);
        }

        [Fact]
        public void EventTypeStringIsCorrect()
        {
            HoundEventTypeMapper.GetString(HoundEventType.Success).ShouldBe("success");
            HoundEventTypeMapper.GetString(HoundEventType.Info).ShouldBe("info");
            HoundEventTypeMapper.GetString(HoundEventType.Warning).ShouldBe("warning");
            HoundEventTypeMapper.GetString(HoundEventType.Error).ShouldBe("error");
        }

        private static HoundEvent GetTestEvent()
        {
            return new HoundEvent
            {
                Title = "Test Event",
                Text = "This is the contents of the test event.",
                AggregationKey = "testevent",
                Host = "Hound-001",
                AlertType = HoundEventType.Success
            };
        }

        private static HoundMetricCollection GetTestMetrics()
        {
            return new HoundMetricCollection
            {
                Title = "test.metric",
                Host = "Hound-001",
                Points = new List<HoundMetricPoint>
                 {
                     new HoundMetricPoint
                     {
                          Timestamp = DateTime.UtcNow.AddSeconds(-30),
                          Value = 11
                     },
                     new HoundMetricPoint
                     {
                          Timestamp = DateTime.UtcNow.AddSeconds(-15),
                          Value = 10
                     },
                     new HoundMetricPoint
                     {
                         Timestamp = DateTime.UtcNow,
                         Value = 12
                     }
                 }
            };
        }
    }
}
