using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hound.Datadog
{
    public class EventRequest
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "host")]
        public string Host { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public IEnumerable<string> Tags { get; set; }

        [JsonProperty(PropertyName = "alert_type")]
        public string AlertType { get; set; }

        [JsonProperty(PropertyName = "aggregation_key")]
        public string AggregationKey { get; set; }

        [JsonProperty(PropertyName = "source_type_name")]
        public string SourceTypeName { get; set; }
    }
}