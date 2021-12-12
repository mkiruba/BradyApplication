using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Helpers;
using Brady.Application.Strategies.Interface;

namespace Brady.Application.Strategies;

public class GasEmissionStrategy : IEmissionStrategy<GasGenerator>
{
    public List<Entities.Output.Day> GetEmissions(GasGenerator generator, ReferenceData referenceData)
    {
        var days = new List<Entities.Output.Day>();
        var emissionFactor = EmissionFactorHelper.GetEmissionFactor(generator, referenceData);
        foreach (var day in generator.Generation.Day)
        {
            var energy = generator.Generation.Day.FirstOrDefault(x => x.Date == day.Date).Energy;
            days.Add(new Entities.Output.Day
            {
                Name = generator.Name,
                Date = day.Date,
                Emission = energy * generator.EmissionsRating * emissionFactor
            });
        }
        return days;
    }
}
