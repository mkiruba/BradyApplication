using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;

namespace Brady.Application.Helpers;

public class EmissionFactorHelper
{
    public static decimal GetEmissionFactor(GeneratorBase generator, ReferenceData referenceData)
    {
        if (generator.GetType() == typeof(GasGenerator))
        {
            return referenceData.Factors.EmissionsFactor.Medium;
        }
        if (generator.GetType() == typeof(CoalGenerator))
        {
            return referenceData.Factors.EmissionsFactor.High;
        }
        if (generator.GetType() == typeof(WindGenerator))
        {
            return 1m;
        }
        throw new ArgumentException("Invalid type");
    }
}
