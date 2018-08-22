using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hound
{
    public interface IExceptionDestination
    {
        Task<HoundResult> Publish(HoundException exception, IEnumerable<string> tags = null);
    }
}