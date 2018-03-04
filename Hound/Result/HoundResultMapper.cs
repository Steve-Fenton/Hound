using Hound.Datadog;
using Newtonsoft.Json;
using System;

namespace Hound.Result
{
    internal static class HoundResultMapper
    {
        internal static HoundResult GetHoundResult(string stringResult, DatadogApi api)
        {
            EventResponse data = JsonConvert.DeserializeObject<EventResponse>(stringResult);

            return new HoundResult
            {
                IsSuccess = api.IsSuccess(data.Status),
                Uri = data.Event.Url
            };
        }

        internal static HoundResult GetSuccessResponse()
        {
            return new HoundResult
            {
                IsSuccess = true,
                Uri = string.Empty
            };
        }

        internal static HoundResult GetFailureResponse(Exception exception)
        {
            return new HoundResult
            {
                IsSuccess = false,
                Uri = string.Empty,
                Error = exception
            };
        }
    }
}