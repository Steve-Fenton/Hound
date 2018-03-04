using Hound.Datadog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Hound.Metrics
{
    internal static class HoundMetricCollectionMapper
    {
        internal static HttpContent GetHttpContent(HoundMetricCollection houndMetricCollection)
        {
            var metricRequest = new MetricRequest
            {
                Series = new List<MetricSeries>
                {
                    new MetricSeries
                    {
                        Host = houndMetricCollection.Host,
                        Title = $"hound.{houndMetricCollection.Title.ToLowerInvariant().Replace(' ', '.')}",
                        Points = houndMetricCollection.Points.Select(p => new List<decimal> { GetPosixDateTime(p.Timestamp), p.Value } as IEnumerable<decimal>)
                    }
                }
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(metricRequest));
            content.Headers.Remove("Content-type");
            content.Headers.Add("Content-type", "application/json");

            return content;
        }

        private static int GetPosixDateTime(DateTime dateTime)
        {
            return (Int32)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
