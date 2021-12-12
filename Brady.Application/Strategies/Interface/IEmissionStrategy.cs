using Brady.Application.Entities.Output;
using Brady.Application.Entities.ReferenceData;

namespace Brady.Application.Strategies.Interface;

public interface IEmissionStrategy<T>
{
    public List<Day> GetEmissions(T generator, ReferenceData referenceData);
}
