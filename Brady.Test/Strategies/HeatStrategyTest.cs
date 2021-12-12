using AutoFixture;
using AutoFixture.AutoMoq;
using Brady.Application.Entities.Input;
using Brady.Application.Strategies;
using Xunit;

namespace Brady.Test.Strategies
{
    public class HeatStrategyTest
    {
        private readonly IFixture _fixture;
        private readonly CoalGenerator _coalGenerator;
        private readonly HeatRateStrategy _heatRateStrategy;

        public HeatStrategyTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _coalGenerator = _fixture.Create<CoalGenerator>();
            _heatRateStrategy = _fixture.Create<HeatRateStrategy>();
        }

        [Fact]
        public void GetHeatRates_Should_Return_Name()
        {                       
            //Act
            var results = _heatRateStrategy.GetHeatRates(_coalGenerator);

            //Assert
            Assert.All(results, x => Assert.Equal(_coalGenerator.Name, x.Name));
        }

        [Fact]
        public void GetHeatRates_Should_Return_HeatRate()
        {
            //Act
            var results = _heatRateStrategy.GetHeatRates(_coalGenerator);

            //Assert            
            var expected = _coalGenerator.TotalHeatInput / _coalGenerator.ActualNetGeneration;
            Assert.All(results, x => Assert.Equal(expected, x.HeatRate));
        }
    }
}