using AutoFixture;
using AutoFixture.AutoMoq;
using Brady.Application.Entities.Input;
using Brady.Application.Entities.ReferenceData;
using Brady.Application.Helpers;
using Xunit;

namespace Brady.Test.Helpers
{
    public class ValueFactorHelperTest
    {
        private readonly IFixture _fixture;

        public ValueFactorHelperTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }
        
        [Fact]
        public void GetValueFactor_GasGenerator_Should_Return_Medium()
        {
            //Arrange
            var generator = _fixture.Create<GasGenerator>();
            var referenceData = _fixture.Create<ReferenceData>();

            //Act
            var result = ValueFactorHelper.GetValueFactor(generator, referenceData);

            //Assert
            Assert.Equal(referenceData.Factors.ValueFactor.Medium, result);
        }

        [Fact]
        public void GetEmissionFactor_CoalGenerator_Should_Return_Medium()
        {
            //Arrange
            var generator = _fixture.Create<CoalGenerator>();
            var referenceData = _fixture.Create<ReferenceData>();

            //Act
            var result = ValueFactorHelper.GetValueFactor(generator, referenceData);

            //Assert
            Assert.Equal(referenceData.Factors.ValueFactor.Medium, result);
        }

        [Fact]
        public void GetEmissionFactor_WindGenerator_OffShore_Should_Return_Low()
        {
            //Arrange
            var generator = _fixture.Build<WindGenerator>()
                .With(x => x.Name, "offshore")
                .Create();
            var referenceData = _fixture.Create<ReferenceData>();

            //Act
            var result = ValueFactorHelper.GetValueFactor(generator, referenceData);

            //Assert
            Assert.Equal(referenceData.Factors.ValueFactor.Low, result);
        }

        [Fact]
        public void GetEmissionFactor_WindGenerator_OnShore_Should_Return_High()
        {
            //Arrange
            var generator = _fixture.Build<WindGenerator>()
               .With(x => x.Name, "onshore")
               .Create();
            var referenceData = _fixture.Create<ReferenceData>();

            //Act
            var result = ValueFactorHelper.GetValueFactor(generator, referenceData);

            //Assert
            Assert.Equal(referenceData.Factors.ValueFactor.High, result);
        }
    }
}
