namespace Hound.Datadog
{
    internal class DatadogMetricApi : DatadogApi
    {
        internal DatadogMetricApi(string apiKey) : base(apiKey)
        { }

        internal string PostMetricUrl
        {
            get
            {
                return $"/api/v1/series?api_key={ApiKey}";
            }
        }
    }
}