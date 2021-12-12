using Brady.Domain.Entities.Output;

namespace Brady.Domain.Strategies.Interface;

public interface IHeatRateStrategy<T>
{
    public List<ActualHeatRate> GetHeatRates(T generator);
}
