using AutoFixture;
using AutoFixture.AutoMoq;
using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Strategies;
using System.Linq;
using Xunit;

namespace Brady.Test.Strategies
{
    public class CoalEmissionStrategyTest
    {
        private readonly IFixture _fixture;
        private readonly CoalGenerator _coalGenerator;
        private readonly ReferenceData _referenceData;
        private readonly CoalEmissionStrategy _coalEmissionStrategy;

        public CoalEmissionStrategyTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _coalGenerator = _fixture.Create<CoalGenerator>();
            _referenceData = _fixture.Create<ReferenceData>();
            _coalEmissionStrategy = _fixture.Create<CoalEmissionStrategy>();
        }

        [Fact]
        public void GetEmissions_Should_Return_Name()
        {                       
            //Act
            var results = _coalEmissionStrategy.GetEmissions(_coalGenerator, _referenceData);

            //Assert
            Assert.All(results, x => Assert.Equal(_coalGenerator.Name, x.Name));
        }

        [Fact]
        public void GetEmissions_Should_Return_Dates()
        {
            //Act
            var results = _coalEmissionStrategy.GetEmissions(_coalGenerator, _referenceData);

            //Assert            
            var expectedDates = _coalGenerator.Generation.Day.Select(x => x.Date);
            var actualDates = results.Select(x => x.Date);
            Assert.Equal(expectedDates, actualDates);
        }

        [Fact]
        public void GetEmissions_Should_Return_Emissions()
        {
            //Act
            var results = _coalEmissionStrategy.GetEmissions(_coalGenerator, _referenceData);

            //Assert
            var expectedEmissions = _coalGenerator.Generation.Day.Select(x => x.Energy * _coalGenerator.EmissionsRating * _referenceData.Factors.EmissionsFactor.High);
            var actualEmissions = results.Select(x => x.Emission);
            Assert.Equal(expectedEmissions, actualEmissions);
        }
    }
}
