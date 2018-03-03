using System;
using System.Net.Http;

namespace Hound.Datadog
{
    internal class DatadogApi
    {
        protected readonly string ApiKey;
        private const string SUCCESS_STATUS = "ok";
        private const string BASE_ADDRESS = "https://app.datadoghq.com/";

        protected DatadogApi(string apiKey)
        {
            ApiKey = apiKey;
        }

        internal HttpClient GetClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(BASE_ADDRESS)
            };
        }

        internal bool IsSuccess(string status)
        {
            return status.Equals(SUCCESS_STATUS, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}