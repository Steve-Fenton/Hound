using Newtonsoft.Json;

namespace Hound.Datadog
{
    public class EventResponseEvent
    {
        [JsonProperty(PropertyName = "date_happened")]
        public long DateHappenedPosix { get; set; }

        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public string Priority { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }

    public class EventResponse
    {
        [JsonProperty(PropertyName = "event")]
        public EventResponseEvent Event { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}