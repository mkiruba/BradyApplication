using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Helpers;
using Brady.Application.Strategies.Interface;

namespace Brady.Application.Strategies;

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
