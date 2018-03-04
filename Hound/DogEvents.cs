using Hound.Datadog;
using Hound.Events;
using Hound.Result;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hound
{
    public class DogEvents : IEventDestination
    {
        private readonly DatadogEventApi _datadogApi;

        public DogEvents(string apiKey)
        {
            _datadogApi = new DatadogEventApi(apiKey);
        }

        public async Task<HoundResult> Publish(HoundEvent houndEvent)
        {
            using (var client = _datadogApi.GetClient())
            {
                try
                {
                    HttpContent content = HoundEventMapper.GetHttpContent(houndEvent);

                    HttpResponseMessage response = await client.PostAsync(_datadogApi.PostEventUrl, content);
                    response.EnsureSuccessStatusCode();

                    return HoundResultMapper.GetHoundResult(await response.Content.ReadAsStringAsync(), _datadogApi);
                }
                catch (HttpRequestException httpRequestException)
                {
                    Trace.WriteLine(httpRequestException.Message);
                    return HoundResultMapper.GetFailureResponse(httpRequestException);
                }
            }
        }
    }
}