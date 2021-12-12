using Brady.Application.Commands.Handlers.Interface;
using Brady.Application.Entities.Input;
using Brady.Application.Entities.Output;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Events;
using Brady.Application.Services.Interface;
using Brady.Application.Strategies.Interface;

namespace Brady.Application.Commands.Handlers;

public class ProcessFileHandler : IProcessFileHandler
{
    private readonly IXmlReadService _xmlReadService;
    private readonly IXmlWriteService<GenerationOutput> _xmlWriteService;
    private readonly IEmissionStrategy<GasGenerator> _gasEmissionStrategy;
    private readonly IEmissionStrategy<CoalGenerator> _coalEmissionStrategy;
    private readonly IHeatRateStrategy<CoalGenerator> _heatRateStrategy;
    private readonly ITotalStrategy<CoalGenerator> _coalTotalStrategy;
    private readonly ITotalStrategy<GasGenerator> _gasTotalStrategy;
    private readonly ITotalStrategy<WindGenerator> _windTotalStrategy;


    public ProcessFileHandler(IXmlReadService xmlReadService,
        IXmlWriteService<GenerationOutput> xmlWriteService,
        IEmissionStrategy<GasGenerator> gasEmissionStrategy,
        IEmissionStrategy<CoalGenerator> coalEmissionStrategy,
        IHeatRateStrategy<CoalGenerator> heatRateStrategy,
        ITotalStrategy<CoalGenerator> coalTotalStrategy,
        ITotalStrategy<GasGenerator> gasTotalStrategy,
        ITotalStrategy<WindGenerator> windTotalStrategy)
    {
        _xmlReadService = xmlReadService;
        _xmlWriteService = xmlWriteService;
        _gasEmissionStrategy = gasEmissionStrategy;
        _coalEmissionStrategy = coalEmissionStrategy;
        _heatRateStrategy = heatRateStrategy;
        _coalTotalStrategy = coalTotalStrategy;
        _gasTotalStrategy = gasTotalStrategy;
        _windTotalStrategy = windTotalStrategy;
    }

    public GenerationOutput Handle(FileCreated request)
    {
        var generationReport = _xmlReadService.Read<GenerationReport>(request.fileName);

        var referenceData = _xmlReadService.Read<ReferenceData>(request.referenceDataFile);
        var generationOutput = new GenerationOutput();

        
        foreach (var windGenerator in generationReport.Wind.WindGenerator)
        {
            generationOutput.Totals.Generator.Add(_windTotalStrategy.GetTotal(windGenerator, referenceData));
        }

        foreach (var gasGenerator in generationReport.Gas.GasGenerator)
        {
            generationOutput.Totals.Generator.Add(_gasTotalStrategy.GetTotal(gasGenerator, referenceData));
            generationOutput.MaxEmissionGenerators.Day.AddRange(_gasEmissionStrategy.GetEmissions(gasGenerator, referenceData));
        }
        foreach (var coalGenerator in generationReport.Coal.CoalGenerator)
        {
            generationOutput.Totals.Generator.Add(_coalTotalStrategy.GetTotal(coalGenerator, referenceData));
            generationOutput.MaxEmissionGenerators.Day.AddRange(_coalEmissionStrategy.GetEmissions(coalGenerator, referenceData));
            generationOutput.ActualHeatRates.ActualHeatRate.AddRange(_heatRateStrategy.GetHeatRates(coalGenerator));
        }

        var fileName = Path.GetFileNameWithoutExtension(request.fileName);
        _xmlWriteService.Write(generationOutput, $"{request.outputPath}/{fileName}-Result.xml");
        return generationOutput;
    }
}
