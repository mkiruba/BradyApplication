using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;

namespace Brady.Application.Helpers;

public class ValueFactorHelper
{
    public static decimal GetValueFactor(GeneratorBase generator, ReferenceData referenceData)
    {
        if (generator.GetType() == typeof(GasGenerator) || generator.GetType() == typeof(CoalGenerator))
        {
            return referenceData.Factors.ValueFactor.Medium;
        }
        if (generator.GetType() == typeof(WindGenerator) && generator.Name.ToLower().Contains("offshore"))
        {
            return referenceData.Factors.ValueFactor.Low;
        }
        if (generator.GetType() == typeof(WindGenerator) && generator.Name.ToLower().Contains("onshore"))
        {
            return referenceData.Factors.ValueFactor.High;
        }
        throw new ArgumentException("Invalid type");
    }
}
