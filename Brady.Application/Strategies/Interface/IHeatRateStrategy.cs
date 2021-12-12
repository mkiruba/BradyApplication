using Brady.Application.Entities.Output;

namespace Brady.Application.Strategies.Interface;

public interface IHeatRateStrategy<T>
{
    public List<ActualHeatRate> GetHeatRates(T generator);
}
