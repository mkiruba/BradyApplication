using Brady.Domain.Entities.Input;
using Brady.Domain.Entities.ReferenceData;
using Brady.Domain.Helpers;
using Brady.Domain.Strategies.Interface;

namespace Brady.Domain.Strategies;

public class CoalTotalStrategy : ITotalStrategy<CoalGenerator>
{
    public Entities.Output.GeneratorBase GetTotal(CoalGenerator generator, ReferenceData referenceData)
    {
        var totalGenerators = new List<Entities.Output.GeneratorBase>();
        var valueFactor = ValueFactorHelper.GetValueFactor(generator, referenceData);
        return new Entities.Output.GeneratorBase
        {
            Name = generator.Name,
            Total = generator.Generation.Day.Sum(x => x.Energy * x.Price * valueFactor)
        };
    }
}
