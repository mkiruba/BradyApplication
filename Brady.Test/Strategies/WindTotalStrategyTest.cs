using AutoFixture;
using AutoFixture.AutoMoq;
using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Strategies;
using System.Linq;
using Xunit;

namespace Brady.Test.Strategies
{
    public class WindTotalStrategyTest
    {
        private readonly IFixture _fixture;
        private readonly WindGenerator _windGenerator;
        private readonly ReferenceData _referenceData;
        private readonly WindTotalStrategy _windTotalStrategy;

        public WindTotalStrategyTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _windGenerator = _fixture.Build<WindGenerator>()
                .With(x => x.Name, "onshore")
                .Create();
            _referenceData = _fixture.Create<ReferenceData>();
            _windTotalStrategy = _fixture.Create<WindTotalStrategy>();
        }

        [Fact]
        public void GetTotal_Should_Return_Name()
        {                       
            //Act
            var result = _windTotalStrategy.GetTotal(_windGenerator, _referenceData);

            //Assert
            Assert.Equal(_windGenerator.Name, result.Name);
        }

        [Fact]
        public void GetTotal_Should_Return_Total()
        {
            //Act
            var result = _windTotalStrategy.GetTotal(_windGenerator, _referenceData);

            //Assert            
            var expectedTotal = _windGenerator.Generation.Day.Sum(x => x.Energy * x.Price * _referenceData.Factors.ValueFactor.High);
            Assert.Equal(result.Total, expectedTotal);
        }
    }
}