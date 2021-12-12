using AutoFixture;
using AutoFixture.AutoMoq;
using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Strategies;
using System.Linq;
using Xunit;

namespace Brady.Test.Strategies
{
    public class GasTotalStrategyTest
    {
        private readonly IFixture _fixture;
        private readonly GasGenerator _gasGenerator;
        private readonly ReferenceData _referenceData;
        private readonly GasTotalStrategy _gasTotalStrategy;

        public GasTotalStrategyTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _gasGenerator = _fixture.Create<GasGenerator>();
            _referenceData = _fixture.Create<ReferenceData>();
            _gasTotalStrategy = _fixture.Create<GasTotalStrategy>();
        }

        [Fact]
        public void GetTotal_Should_Return_Name()
        {                       
            //Act
            var result = _gasTotalStrategy.GetTotal(_gasGenerator, _referenceData);

            //Assert
            Assert.Equal(_gasGenerator.Name, result.Name);
        }

        [Fact]
        public void GetTotal_Should_Return_Total()
        {
            //Act
            var result = _gasTotalStrategy.GetTotal(_gasGenerator, _referenceData);

            //Assert            
            var expectedTotal = _gasGenerator.Generation.Day.Sum(x => x.Energy * x.Price * _referenceData.Factors.ValueFactor.Medium);
            Assert.Equal(result.Total, expectedTotal);
        }
    }
}