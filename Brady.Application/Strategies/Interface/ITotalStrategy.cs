using Brady.Application.Entities.Output;
using Brady.Application.Entities.ReferenceData;

namespace Brady.Application.Strategies.Interface;

public interface ITotalStrategy<T>
{
    public GeneratorBase GetTotal(T generator, ReferenceData referenceData);
}
