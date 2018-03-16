# Hound

A C# library for publishing events and metrics to Datadog via the Datadog API.

Includes a simple exception logger that logs directly to Datadog, in a swappable way if you change your strategy later.

![Hound](https://raw.githubusercontent.com/Steve-Fenton/Hound/master/Hound/hound.png)

## LogHound

You can call LogHound to store exceptions as simply as:

    LogHound.LogException(apiKey, ex);

A full example is below.

    try
    {
        throw new AuthorException("Sheridan Le Fanu");
    }
    catch (HoundException ex)
    {
        LogHound.LogException(apiKey, ex);
    }
    catch (Exception ex)
    {
        LogHound.LogException(apiKey, new HoundException("Unexpected Exception!", ex));
    }

To isolate yourself from ```LogHound```, in case you decide to use something else later; isolate yourself from the specific ```HoundException``` type by creating your own tree of exception types:

    public class MyCompanyException
        : HoundException
    {
        public MyCompanyException(string message)
            : base(message)
        {
            Severity = HoundEventType.Error;
        }
    }

You can simply replace ```HoundException``` with ```ApplicationException``` later if you want to.

### Severity

You may find it useful to have errors of different severities. LogHound allows success, info, warning, error.

You can set this in the constructor of your custom exception types.

    public class AuthorException
        : HoundException
    {
        public AuthorException(string author)
            : base($"Error with author! The author is {author}.")
        {
            Severity = HoundEventType.Error;
        }
    }

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

    IEventDestination target = new DogEvents(apiKey);
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

    IMetricDestination target = new DogMetrics(apiKey);
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
