using Brady.Domain.Entities.Output;
using Brady.Domain.Entities.ReferenceData;

namespace Brady.Domain.Strategies.Interface;

public interface ITotalStrategy<T>
{
    public GeneratorBase GetTotal(T generator, ReferenceData referenceData);
}
