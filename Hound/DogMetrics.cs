using Hound.Datadog;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hound
{
    public class DogMetrics : IMetricDestination
    {
        private readonly DatadogMetricApi _datadogApi;

        public DogMetrics(string apiKey)
        {
            _datadogApi = new DatadogMetricApi(apiKey);
        }

        public async Task<HoundResult> RaiseMetric(HoundMetricCollection houndMetricCollection)
        {
            using (var client = _datadogApi.GetClient())
            {
                try
                {
                    HttpContent content = HoundMetricCollectionMapper.GetHttpContent(houndMetricCollection);

                    HttpResponseMessage response = await client.PostAsync(_datadogApi.PostMetricUrl, content);
                    response.EnsureSuccessStatusCode();

                    return HoundResultMapper.GetSuccessResponse();
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