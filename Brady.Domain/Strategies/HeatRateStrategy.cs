using Brady.Domain.Entities.Input;
using Brady.Domain.Entities.Output;
using Brady.Domain.Strategies.Interface;

namespace Brady.Domain.Strategies;

public class HeatRateStrategy : IHeatRateStrategy<CoalGenerator>
{
    public List<ActualHeatRate> GetHeatRates(CoalGenerator generator)
    {
        return new List<ActualHeatRate>
            {
                new ActualHeatRate
                {
                    Name = generator.Name,
                    HeatRate = generator.TotalHeatInput / generator.ActualNetGeneration
                }
            };
    }
}
