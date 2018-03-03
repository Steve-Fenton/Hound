# Hound

A C# library for publishing events and metrics to Datadog via the Datadog API.

## Events

Create an event:

    HoundEvent data = new HoundEvent
    {
        Title = "Test Event",
        Text = "This is the contents of the test event.",
        AggregationKey = "testevent",
        Host = "Hound-001",
        AlertType = HoundEventType.Success
    };

Publish the event:

    DogEvents target = new DogEvents(apiKey);
    HoundResult eventResponse = await target.Publish(data);

The ```eventResponse.IsSuccess``` boolean indicates success, and if this is false, you can check out the exception under ```eventResponse.Error```.

A typical strategy would swallow a failure to publish an event, but place this information somewhere transient in case you need to investigate lack of events.

## Metrics

Create a metric collection:

    HoundMetricCollection data = new HoundMetricCollection
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

Publish the metrics:

    DogMetrics target = new DogMetrics(apiKey);
    HoundResult eventResponse = await target.RaiseMetric(data);

The ```eventResponse.IsSuccess``` boolean indicates success, and if this is false, you can check out the exception under ```eventResponse.Error```.

### Metric Notes

Passing a collection of metrics is more efficient than sending one at a time.

The recommended gap between metrics is 15 seconds or more (use your judgement to collect it just often enough to make changes visible, if you measure something that changes once an hour; you don't need to collect it every 15 seconds!)

## General

As the two areas are publish-only, you only need to supply a Datadog API key and Hound will take care of the REST. You don't need an application key for these methods.

The doc comments on the data objects will guide you!

https://github.com/Steve-Fenton/Hound/blob/master/Hound/Events/HoundEvent.cs

https://github.com/Steve-Fenton/Hound/blob/master/Hound/Metrics/HoundMetric.cs