using Hound.Result;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hound
{
    public static class LogHound
    {
        public static HoundResult LogException(string apiKey, Exception exception)
        {
            try
            {
                HoundException houndException = CastException(exception);
                return Log(apiKey).Publish(houndException).Result;
            }
            catch (Exception ex)
            {
                return HoundResultMapper.GetFailureResponse(ex);
            }
        }     

        public static HoundResult LogException(string apiKey, Exception exception, IEnumerable<string> tags)
        {
            try
            {
                HoundException houndException = CastException(exception);
                return Log(apiKey).Publish(houndException, tags).Result;
            }
            catch (Exception ex)
            {
                return HoundResultMapper.GetFailureResponse(ex);
            }
        }

        private static HoundException CastException(Exception exception)
        {
            HoundException houndException = exception as HoundException;

            if (houndException == null)
            {
                houndException = new HoundException(exception);
            }

            return houndException;
        }

        private static IExceptionDestination Log(string apiKey)
        {
            IEventDestination eventDestination = new DogEvents(apiKey);
            IExceptionDestination exceptionDestination = new DogExceptions(eventDestination);
            return Task.Run(async() => await exceptionDestination.Publish(exception)).Result;
        }
    }
}