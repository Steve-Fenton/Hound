using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Hound.Datadog
{
    public class RateLimit
    {
        public string Limit { get; set; }

        public string Period { get; set; }

        public string Remaining { get; set; }

        public string Reset { get; set; }

        public static RateLimit FromHeaders(HttpResponseHeaders headers)
        {
            RateLimit rateLimit = new RateLimit
            {
                Limit = GetHeaderValue(headers, "X-RateLimit-Limit"),
                Period = GetHeaderValue(headers, "X-RateLimit-Period"),
                Remaining = GetHeaderValue(headers, "X-RateLimit-Remaining "),
                Reset = GetHeaderValue(headers, "X-RateLimit-Reset ")
            };

            return rateLimit;
        }

        private static string GetHeaderValue(HttpResponseHeaders headers, string key)
        {
            IEnumerable<string> items = new List<string>();

            if (headers.TryGetValues(key, out items))
            {
                return items.FirstOrDefault();
            }

            return string.Empty;
        }
    }
}