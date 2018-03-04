using Hound.Result;
using System;

namespace Hound
{
    public static class LogHound
    {
        public static HoundResult LogException(string apiKey, HoundException exception)
        {
            try
            {
                IEventDestination eventDestination = new DogEvents(apiKey);
                IExceptionDestination exceptionDestination = new DogExceptions(eventDestination);
                return exceptionDestination.Publish(exception).Result;
            }
            catch(Exception ex)
            {
                return HoundResultMapper.GetFailureResponse(ex);
            }
        }
    }
}