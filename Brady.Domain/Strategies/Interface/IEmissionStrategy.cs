using Brady.Domain.Entities.ReferenceData;

namespace Brady.Domain.Strategies.Interface;

public interface IEmissionStrategy<T>
{
    public List<Entities.Output.Day> GetEmissions(T generator, ReferenceData referenceData);
}
