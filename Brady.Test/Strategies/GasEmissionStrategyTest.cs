using AutoFixture;
using AutoFixture.AutoMoq;
using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Strategies;
using System.Linq;
using Xunit;

namespace Brady.Test.Strategies
{
    public class GasEmissionStrategyTest
    {
        private readonly IFixture _fixture;
        private readonly GasGenerator _gasGenerator;
        private readonly ReferenceData _referenceData;
        private readonly GasEmissionStrategy _gasEmissionStrategy;

        public GasEmissionStrategyTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _gasGenerator = _fixture.Create<GasGenerator>();
            _referenceData = _fixture.Create<ReferenceData>();
            _gasEmissionStrategy = _fixture.Create<GasEmissionStrategy>();
        }

        [Fact]
        public void GetEmissions_Should_Return_Name()
        {                       
            //Act
            var results = _gasEmissionStrategy.GetEmissions(_gasGenerator, _referenceData);

            //Assert
            Assert.All(results, x => Assert.Equal(_gasGenerator.Name, x.Name));
        }

        [Fact]
        public void GetEmissions_Should_Return_Dates()
        {
            //Act
            var results = _gasEmissionStrategy.GetEmissions(_gasGenerator, _referenceData);

            //Assert            
            var expectedDates = _gasGenerator.Generation.Day.Select(x => x.Date);
            var actualDates = results.Select(x => x.Date);
            Assert.Equal(expectedDates, actualDates);
        }

        [Fact]
        public void GetEmissions_Should_Return_Emissions()
        {
            //Act
            var results = _gasEmissionStrategy.GetEmissions(_gasGenerator, _referenceData);

            //Assert
            var expectedEmissions = _gasGenerator.Generation.Day.Select(x => x.Energy * _gasGenerator.EmissionsRating * _referenceData.Factors.EmissionsFactor.Medium);
            var actualEmissions = results.Select(x => x.Emission);
            Assert.Equal(expectedEmissions, actualEmissions);
        }
    }
}
