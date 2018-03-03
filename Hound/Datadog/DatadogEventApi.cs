namespace Hound.Datadog
{
    internal class DatadogEventApi : DatadogApi
    {
        internal DatadogEventApi(string apiKey) : base(apiKey)
        { }

        internal string PostEventUrl
        {
            get
            {
                return $"/api/v1/events?api_key={ApiKey}";
            }
        }
    }
}