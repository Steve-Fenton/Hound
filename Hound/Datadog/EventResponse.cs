using Newtonsoft.Json;

namespace Hound.Datadog
{
    public class EventResponse
    {
        [JsonProperty(PropertyName = "event")]
        public EventResponseEvent Event { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}