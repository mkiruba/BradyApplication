using AutoFixture;
using AutoFixture.AutoMoq;
using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Helpers;
using Xunit;

namespace Brady.Test.Helpers
{
    public class EmissionFactorHelperTest
    {
        private readonly IFixture _fixture;

        public EmissionFactorHelperTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }
        
        [Fact]
        public void GetEmissionFactor_GasGenerator_Should_Return_Medium()
        {
            //Arrange
            var generator = _fixture.Create<GasGenerator>();
            var referenceData = _fixture.Create<ReferenceData>();

            //Act
            var result = EmissionFactorHelper.GetEmissionFactor(generator, referenceData);

            //Assert
            Assert.Equal(referenceData.Factors.EmissionsFactor.Medium, result);
        }

        [Fact]
        public void GetEmissionFactor_CoalGenerator_Should_Return_High()
        {
            //Arrange
            var generator = _fixture.Create<CoalGenerator>();
            var referenceData = _fixture.Create<ReferenceData>();

            //Act
            var result = EmissionFactorHelper.GetEmissionFactor(generator, referenceData);

            //Assert
            Assert.Equal(referenceData.Factors.EmissionsFactor.High, result);
        }

        [Fact]
        public void GetEmissionFactor_WindGenerator_Should_Return_One()
        {
            //Arrange
            var generator = _fixture.Create<WindGenerator>();
            var referenceData = _fixture.Create<ReferenceData>();

            //Act
            var result = EmissionFactorHelper.GetEmissionFactor(generator, referenceData);

            //Assert
            Assert.Equal(1m, result);
        }
    }
}
