using System.Threading.Tasks;

namespace Hound
{
    public interface IMetricDestination
    {
        Task<HoundResult> RaiseMetric(HoundMetricCollection houndMetricCollection);
    }
}