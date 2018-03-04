using Hound.Datadog;
using Newtonsoft.Json;
using System.Net.Http;

namespace Hound.Events
{
    internal static class HoundEventMapper
    {
        internal static HttpContent GetHttpContent(HoundEvent houndEvent)
        {
            var eventRequest = new EventRequest
            {
                AggregationKey = houndEvent.AggregationKey,
                AlertType = HoundEventTypeMapper.GetString(houndEvent.AlertType),
                Host = houndEvent.Host,
                SourceTypeName = houndEvent.SourceTypeName,
                Text = houndEvent.Text,
                Title = houndEvent.Title
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(eventRequest));
            content.Headers.Remove("Content-type");
            content.Headers.Add("Content-type", "application/json");

            return content;
        }
    }
}