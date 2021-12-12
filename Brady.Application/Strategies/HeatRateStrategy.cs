using Brady.Application.Entities.Input;
using Brady.Application.Entities.Output;
using Brady.Application.Strategies.Interface;

namespace Brady.Application.Strategies;

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
