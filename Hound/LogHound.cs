using Hound.Result;
using System;

namespace Hound
{
    public static class LogHound
    {
        public static HoundResult LogException(string apiKey, Exception exception)
        {
            try
            {
                HoundException houndException = exception as HoundException;

                if (houndException == null)
                {
                    houndException = new HoundException(exception);
                }

                return Log(apiKey, houndException);
            }
            catch (Exception ex)
            {
                return HoundResultMapper.GetFailureResponse(ex);
            }
        }

        private static HoundResult Log(string apiKey, HoundException exception)
        {
            IEventDestination eventDestination = new DogEvents(apiKey);
            IExceptionDestination exceptionDestination = new DogExceptions(eventDestination);
            return exceptionDestination.Publish(exception).Result;
        }
    }
}