using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Hound.Tests
{
    public class DogTests
    {
        private readonly string _testApiKey;

        public DogTests()
        {
            ApiKey key = new TestApiKey();
            _testApiKey = key.GetApiKey();
        }

        [Fact]
        public async Task RaiseErrorShouldBeSuccessful()
        {
            IEventDestination target = new DogEvents(_testApiKey);
            HoundEvent data = GetTestEvent();

            HoundResult eventResponse = await target.Publish(data);

            eventResponse.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public async Task RaiseMetricShouldBeSuccessful()
        {
            IMetricDestination target = new DogMetrics(_testApiKey);
            HoundMetricCollection data = GetTestMetrics();

            HoundResult eventResponse = await target.RaiseMetric(data);

            eventResponse.IsSuccess.ShouldBeTrue();
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
