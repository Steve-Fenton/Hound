using Hound.Datadog;
using Hound.Metrics;
using Hound.Result;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hound
{
    public class DogMetrics
        : IMetricDestination
    {
        private readonly DatadogMetricApi _datadogApi;
        private readonly string _prefix;

        public DogMetrics(string apiKey, string prefix = "hound")
        {
            _datadogApi = new DatadogMetricApi(apiKey);
            _prefix = prefix.ToLowerInvariant();
        }

        public async Task<HoundResult> RaiseMetric(HoundMetricCollection houndMetricCollection)
        {
            using (var client = _datadogApi.GetClient())
            {
                try
                {
                    HttpContent content = HoundMetricCollectionMapper.GetHttpContent(houndMetricCollection, _prefix);

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