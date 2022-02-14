using FluentAssertions;
using RJSilvas.MoneyLib.Core;
using Xunit;

namespace RJSilvas.MoneyLib.Tests
{
    [UseCulture("pt-BR")]
    public class ReaisTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(1500)]
        public void Reais_ShouldCreateANewBRLInstance_WhenAmountIsGreaterThanZero(decimal amount)
        {
            // Arrange
            Money sut = Money.Reais(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(amount);
            sut.Currency.Should().Be(Currency.BRL);
            if (amount > 1000)
                sut.ToString().Should().Be($"R$ 1.500,00");
            else
                sut.ToString().Should().Be($"R$ {amount},00");
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-10)]
        [InlineData(-15)]
        public void Reais_ShouldCreateANewBRLInstance_WhenAmountIsLessThanZero(decimal amount)
        {
            // Arrange
            Money sut = Money.Reais(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(amount);
            sut.Currency.Should().Be(Currency.BRL);
            sut.ToString().Should().Be($"-R$ {-amount},00");
        }

        [Fact]
        public void Reais_ShouldCreateANewBRLInstance_WhenAmountIsZero()
        {
            // Arrange
            Money sut = Money.Reais(0);

            // Act

            // Assert
            sut.Amount.Should().Be(0);
            sut.Currency.Should().Be(Currency.BRL);
            sut.ToString().Should().Be($"R$ 0,00");
        }

        [Theory]
        [InlineData(0.12)]
        [InlineData(0.123)]
        [InlineData(0.124)]
        [InlineData(0.12409)]
        public void Reais_ShouldCreateANewBRLInstanceRoundingToCentsKeepingTheLastDigit_WhenTheLastDigitIsLowerThanFive(decimal amount)
        {
            // Arrange
            Money sut = Money.Reais(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(0.12M);
            sut.Currency.Should().Be(Currency.BRL);
            sut.ToString().Should().Be($"R$ 0,12");
        }

        [Theory]
        [InlineData(0.126)]
        [InlineData(0.127)]
        [InlineData(0.128)]
        public void Reais_ShouldCreateANewBRLInstanceRoundingToCentsRoudingUpTheLastDigit_WhenTheLastDigitIsGreaterThanFive(decimal amount)
        {
            // Arrange
            Money sut = Money.Reais(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(0.13M);
            sut.Currency.Should().Be(Currency.BRL);
            sut.ToString().Should().Be($"R$ 0,13");
        }

        [Fact]
        public void Reais_ShouldCreateANewBRLInstanceRoundingToCentsRoudingUpTheLastDigit_WhenTheLastDigitIsEqualThanFive()
        {
            // Arrange
            Money sut = Money.Reais(0.125M);

            // Act

            // Assert
            sut.Amount.Should().Be(0.13M);
            sut.Currency.Should().Be(Currency.BRL);
            sut.ToString().Should().Be($"R$ 0,13");
        }

        [Fact]
        public void Reais_ShouldHaveTwoDecimalPlaces()
        {
            // Arrange
            Money sut = Money.Reais(0);

            // Act

            // Assert
            sut.DecimalPlaces.Should().Be(2);
        }

        [Fact]
        public void Reais_ShouldHaveOneCentAsSmallestValue()
        {
            // Arrange
            Money sut = Money.Reais(0);

            // Act

            // Assert
            sut.SmallestAmount.Should().Be(0.01m);
        }        
    }
}
