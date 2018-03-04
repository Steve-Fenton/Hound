using System.Threading.Tasks;

namespace Hound
{
    public interface IEventDestination
    {
        Task<HoundResult> Publish(HoundEvent houndEvent);
    }
}