using AutoFixture;
using AutoFixture.AutoMoq;
using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Strategies;
using System.Linq;
using Xunit;

namespace Brady.Test.Strategies
{
    public class CoalTotalStrategyTest
    {
        private readonly IFixture _fixture;
        private readonly CoalGenerator _coalGenerator;
        private readonly ReferenceData _referenceData;
        private readonly CoalTotalStrategy _coalTotalStrategy;

        public CoalTotalStrategyTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _coalGenerator = _fixture.Create<CoalGenerator>();
            _referenceData = _fixture.Create<ReferenceData>();
            _coalTotalStrategy = _fixture.Create<CoalTotalStrategy>();
        }

        [Fact]
        public void GetTotal_Should_Return_Name()
        {                       
            //Act
            var result = _coalTotalStrategy.GetTotal(_coalGenerator, _referenceData);

            //Assert
            Assert.Equal(_coalGenerator.Name, result.Name);
        }

        [Fact]
        public void GetTotal_Should_Return_Total()
        {
            //Act
            var result = _coalTotalStrategy.GetTotal(_coalGenerator, _referenceData);

            //Assert            
            var expectedTotal = _coalGenerator.Generation.Day.Sum(x => x.Energy * x.Price * _referenceData.Factors.ValueFactor.Medium);
            Assert.Equal(result.Total, expectedTotal);
        }
    }
}