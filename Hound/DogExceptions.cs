using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hound
{
    public class DogExceptions :
        IExceptionDestination
    {
        private readonly IEventDestination _eventDestination;

        public DogExceptions(IEventDestination eventDestination)
        {
            _eventDestination = eventDestination;
        }

        public async Task<HoundResult> Publish(HoundException exception, IEnumerable<string> tags = null)
        {
            HoundEvent houndEvent = new HoundEvent
            {
                AggregationKey = exception.GetType().FullName,
                AlertType = exception.Severity,
                Host = exception.HostName,
                Text = exception.ToString(),
                Title = exception.Message,
                Tags = tags ?? new List<string>()
            };

            return await _eventDestination.Publish(houndEvent);
        }
    }
}