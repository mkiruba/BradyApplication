using Brady.Application.Entities.Output;
using Brady.Application.Events;

namespace Brady.Application.Commands.Handlers.Interface;

public interface IProcessFileHandler
{
    public GenerationOutput Handle(FileCreated request);
}