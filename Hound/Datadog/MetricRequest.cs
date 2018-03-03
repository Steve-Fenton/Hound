using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hound.Datadog
{
    public class MetricRequest
    {
        [JsonProperty(PropertyName = "series")]
        public IEnumerable<MetricSeries> Series { get; set; }
    }

    public class MetricSeries
    {
        [JsonProperty(PropertyName = "metric")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "points")]
        public IEnumerable<IEnumerable<decimal>> Points { get; set; }

        [JsonProperty(PropertyName = "host")]
        public string Host { get; set; }
    }
}