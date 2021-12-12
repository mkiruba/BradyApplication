using AutoFixture;
using AutoFixture.AutoMoq;
using Brady.Application.Commands.Handlers;
using Brady.Application.Entities.Input;
using Brady.Application.Entities.Output;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Events;
using Brady.Application.Services.Interface;
using Brady.Application.Strategies.Interface;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Brady.Test.Application;

public class ProcessFileHandlerTest
{
    private int noOfMockItems = 3;
    private readonly IFixture _fixture;
    private readonly FileCreated _fileCreated;
    private Mock<IXmlReadService> _mockXmlReadService;
    private Mock<IXmlWriteService<GenerationOutput>> _mockXmlWriteService;
    private Mock<IEmissionStrategy<GasGenerator>> _mockGasEmissionStrategy;
    private Mock<IEmissionStrategy<CoalGenerator>> _mockCoalEmissionStrategy;
    private Mock<IHeatRateStrategy<CoalGenerator>> _mockCoalHeatStrategy;
    private Mock<ITotalStrategy<CoalGenerator>> _mockCoalTotalStrategy;
    private Mock<ITotalStrategy<GasGenerator>> _mockGasTotalStrategy;
    private Mock<ITotalStrategy<WindGenerator>> _mockWindTotalStrategy;

    private readonly GenerationReport _generationReport;
    private readonly ReferenceData _referenceData;
    private readonly Brady.Application.Entities.Output.GeneratorBase _generator;
    private readonly List<Brady.Application.Entities.Output.Day> _days;
    private readonly List<ActualHeatRate> _actualHeatRates;

    public ProcessFileHandlerTest()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());

        _fileCreated = _fixture.Create<FileCreated>(); ;

        _mockXmlReadService = new Mock<IXmlReadService>();
        _mockXmlWriteService = new Mock<IXmlWriteService<GenerationOutput>>();
        _mockGasEmissionStrategy = new Mock<IEmissionStrategy<GasGenerator>>();
        _mockCoalEmissionStrategy = new Mock<IEmissionStrategy<CoalGenerator>>();
        _mockCoalHeatStrategy = new Mock<IHeatRateStrategy<CoalGenerator>>();
        _mockCoalTotalStrategy = new Mock<ITotalStrategy<CoalGenerator>>();
        _mockGasTotalStrategy = new Mock<ITotalStrategy<GasGenerator>>();
        _mockWindTotalStrategy = new Mock<ITotalStrategy<WindGenerator>>();

        _fixture.Inject(_mockXmlReadService.Object);
        _fixture.Inject(_mockXmlWriteService.Object);
        _fixture.Inject(_mockGasEmissionStrategy.Object);
        _fixture.Inject(_mockCoalEmissionStrategy.Object);
        _fixture.Inject(_mockCoalHeatStrategy.Object);
        _fixture.Inject(_mockCoalTotalStrategy.Object);
        _fixture.Inject(_mockGasTotalStrategy.Object);
        _fixture.Inject(_mockWindTotalStrategy.Object);

        _generationReport = _fixture.Create<GenerationReport>();
        _referenceData = _fixture.Create<ReferenceData>();
        _generator = _fixture.Create<Brady.Application.Entities.Output.GeneratorBase>();
        _days = _fixture.CreateMany<Brady.Application.Entities.Output.Day>(noOfMockItems).ToList();
        _actualHeatRates = _fixture.CreateMany<ActualHeatRate>(noOfMockItems).ToList();
    }

    [Fact]
    public void HandleTest_Should_CreateOutput()
    {
        //Arrange
        _mockXmlReadService.Setup(x => x.Read<GenerationReport>(It.IsAny<string>())).Returns(_generationReport);
        _mockXmlReadService.Setup(x => x.Read<ReferenceData>(It.IsAny<string>())).Returns(_referenceData);
        _mockWindTotalStrategy.Setup(x => x.GetTotal(It.IsAny<WindGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasTotalStrategy.Setup(x => x.GetTotal(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockCoalTotalStrategy.Setup(x => x.GetTotal(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalHeatStrategy.Setup(x => x.GetHeatRates(It.IsAny<CoalGenerator>())).Returns(_actualHeatRates);
        _mockXmlWriteService.Setup(x => x.Write(It.IsAny<GenerationOutput>(), It.IsAny<string>()));

        var processFileHandler = _fixture.Create<ProcessFileHandler>();

        //Act
        processFileHandler.Handle(_fileCreated);

        //Assert
        _mockXmlWriteService.Verify(x => x.Write(It.IsAny<GenerationOutput>(), It.IsAny<string>()), Times.Once());
    }

    [Fact]
    public void HandleTest_Should_CreateTotalsGenerators_VerifyCount()
    {
        //Arrange
        _mockXmlReadService.Setup(x => x.Read<GenerationReport>(It.IsAny<string>())).Returns(_generationReport);
        _mockXmlReadService.Setup(x => x.Read<ReferenceData>(It.IsAny<string>())).Returns(_referenceData);
        _mockWindTotalStrategy.Setup(x => x.GetTotal(It.IsAny<WindGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasTotalStrategy.Setup(x => x.GetTotal(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockCoalTotalStrategy.Setup(x => x.GetTotal(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalHeatStrategy.Setup(x => x.GetHeatRates(It.IsAny<CoalGenerator>())).Returns(_actualHeatRates);
        _mockXmlWriteService.Setup(x => x.Write(It.IsAny<GenerationOutput>(), It.IsAny<string>()));

        var processFileHandler = _fixture.Create<ProcessFileHandler>();

        //Act
        var results = processFileHandler.Handle(_fileCreated);

        //Assert
        Assert.Equal(_generationReport.Coal.CoalGenerator.Count +
            _generationReport.Gas.GasGenerator.Count +
            _generationReport.Wind.WindGenerator.Count,
            results.Totals.Generator.Count);        
    }

    [Fact]
    public void HandleTest_Should_CreateTotalsGenerators_VerifyTotal()
    {
        //Arrange
        _mockXmlReadService.Setup(x => x.Read<GenerationReport>(It.IsAny<string>())).Returns(_generationReport);
        _mockXmlReadService.Setup(x => x.Read<ReferenceData>(It.IsAny<string>())).Returns(_referenceData);
        _mockWindTotalStrategy.Setup(x => x.GetTotal(It.IsAny<WindGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasTotalStrategy.Setup(x => x.GetTotal(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockCoalTotalStrategy.Setup(x => x.GetTotal(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalHeatStrategy.Setup(x => x.GetHeatRates(It.IsAny<CoalGenerator>())).Returns(_actualHeatRates);
        _mockXmlWriteService.Setup(x => x.Write(It.IsAny<GenerationOutput>(), It.IsAny<string>()));

        var processFileHandler = _fixture.Create<ProcessFileHandler>();

        //Act
        var results = processFileHandler.Handle(_fileCreated);

        //Assert
        Assert.All(results.Totals.Generator, x => Assert.Equal(_generator.Total, x.Total));
    }

    [Fact]
    public void HandleTest_Should_CreateMaxEmissionGenerators_VerifyCount()
    {
        //Arrange
        _mockXmlReadService.Setup(x => x.Read<GenerationReport>(It.IsAny<string>())).Returns(_generationReport);
        _mockXmlReadService.Setup(x => x.Read<ReferenceData>(It.IsAny<string>())).Returns(_referenceData);
        _mockWindTotalStrategy.Setup(x => x.GetTotal(It.IsAny<WindGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasTotalStrategy.Setup(x => x.GetTotal(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockCoalTotalStrategy.Setup(x => x.GetTotal(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalHeatStrategy.Setup(x => x.GetHeatRates(It.IsAny<CoalGenerator>())).Returns(_actualHeatRates);
        _mockXmlWriteService.Setup(x => x.Write(It.IsAny<GenerationOutput>(), It.IsAny<string>()));

        var processFileHandler = _fixture.Create<ProcessFileHandler>();

        //Act
        var results = processFileHandler.Handle(_fileCreated);

        //Assert
        Assert.Equal(_days.Count * noOfMockItems * 2,
            results.MaxEmissionGenerators.Day.Count);
    }

    [Fact]
    public void HandleTest_Should_CreateTotalsGenerators_VerifyEmissionGenerator()
    {
        //Arrange
        _mockXmlReadService.Setup(x => x.Read<GenerationReport>(It.IsAny<string>())).Returns(_generationReport);
        _mockXmlReadService.Setup(x => x.Read<ReferenceData>(It.IsAny<string>())).Returns(_referenceData);
        _mockWindTotalStrategy.Setup(x => x.GetTotal(It.IsAny<WindGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasTotalStrategy.Setup(x => x.GetTotal(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockCoalTotalStrategy.Setup(x => x.GetTotal(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalHeatStrategy.Setup(x => x.GetHeatRates(It.IsAny<CoalGenerator>())).Returns(_actualHeatRates);
        _mockXmlWriteService.Setup(x => x.Write(It.IsAny<GenerationOutput>(), It.IsAny<string>()));

        var processFileHandler = _fixture.Create<ProcessFileHandler>();

        //Act
        var results = processFileHandler.Handle(_fileCreated);

        //Assert        
        var expected = _days.Select(x => x.Emission);
        var actual = results.MaxEmissionGenerators.Day.Select(x => x.Emission).Take(expected.Count());
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HandleTest_Should_CreateActualHeatRates_VerifyCount()
    {
        //Arrange
        _mockXmlReadService.Setup(x => x.Read<GenerationReport>(It.IsAny<string>())).Returns(_generationReport);
        _mockXmlReadService.Setup(x => x.Read<ReferenceData>(It.IsAny<string>())).Returns(_referenceData);
        _mockWindTotalStrategy.Setup(x => x.GetTotal(It.IsAny<WindGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasTotalStrategy.Setup(x => x.GetTotal(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockCoalTotalStrategy.Setup(x => x.GetTotal(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalHeatStrategy.Setup(x => x.GetHeatRates(It.IsAny<CoalGenerator>())).Returns(_actualHeatRates);
        _mockXmlWriteService.Setup(x => x.Write(It.IsAny<GenerationOutput>(), It.IsAny<string>()));

        var processFileHandler = _fixture.Create<ProcessFileHandler>();

        //Act
        var results = processFileHandler.Handle(_fileCreated);

        //Assert
        Assert.Equal(_actualHeatRates.Count * noOfMockItems,
            results.ActualHeatRates.ActualHeatRate.Count);
    }

    [Fact]
    public void HandleTest_Should_CreateTotalsGenerators_VerifyActualHeatRates()
    {
        //Arrange
        _mockXmlReadService.Setup(x => x.Read<GenerationReport>(It.IsAny<string>())).Returns(_generationReport);
        _mockXmlReadService.Setup(x => x.Read<ReferenceData>(It.IsAny<string>())).Returns(_referenceData);
        _mockWindTotalStrategy.Setup(x => x.GetTotal(It.IsAny<WindGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasTotalStrategy.Setup(x => x.GetTotal(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockCoalTotalStrategy.Setup(x => x.GetTotal(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_generator);
        _mockGasEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<GasGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalEmissionStrategy.Setup(x => x.GetEmissions(It.IsAny<CoalGenerator>(), It.IsAny<ReferenceData>())).Returns(_days);
        _mockCoalHeatStrategy.Setup(x => x.GetHeatRates(It.IsAny<CoalGenerator>())).Returns(_actualHeatRates);
        _mockXmlWriteService.Setup(x => x.Write(It.IsAny<GenerationOutput>(), It.IsAny<string>()));

        var processFileHandler = _fixture.Create<ProcessFileHandler>();

        //Act
        var results = processFileHandler.Handle(_fileCreated);

        //Assert
        var expected = _actualHeatRates.Select(x => x.HeatRate);
        var actual = results.ActualHeatRates.ActualHeatRate.Select(x => x.HeatRate).Take(expected.Count());
        Assert.Equal(expected, actual);
    }
}
