using FluentAssertions;
using RJSilvas.MoneyLib.Core;
using Xunit;

namespace RJSilvas.MoneyLib.Tests
{
    [UseCulture("pt-BR")]
    public class EurosTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(1500)]
        public void Euros_ShouldCreateANewEURInstance_WhenAmountIsGreaterThanZero(decimal amount)
        {
            // Arrange
            Money sut = Money.Euros(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(amount);
            sut.Currency.Should().Be(Currency.EUR);
            if (amount > 1000)
                sut.ToString().Should().Be($"1 500,00 €");
            else
                sut.ToString().Should().Be($"{amount},00 €");
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-10)]
        [InlineData(-15)]
        public void Euros_ShouldCreateANewEURInstance_WhenAmountIsLessThanZero(decimal amount)
        {
            // Arrange
            Money sut = Money.Euros(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(amount);
            sut.Currency.Should().Be(Currency.EUR);
            sut.ToString().Should().Be($"-{-amount},00 €");
        }

        [Fact]
        public void Euros_ShouldCreateANewEURInstance_WhenAmountIsZero()
        {
            // Arrange
            Money sut = Money.Euros(0);

            // Act

            // Assert
            sut.Amount.Should().Be(0);
            sut.Currency.Should().Be(Currency.EUR);
            sut.ToString().Should().Be($"0,00 €");
        }

        [Theory]
        [InlineData(0.12)]
        [InlineData(0.123)]
        [InlineData(0.124)]
        [InlineData(0.12409)]
        public void Euros_ShouldCreateANewEURInstanceRoundingToCentsKeepingTheLastDigit_WhenTheLastDigitIsLowerThanFive(decimal amount)
        {
            // Arrange
            Money sut = Money.Euros(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(0.12M);
            sut.Currency.Should().Be(Currency.EUR);
            sut.ToString().Should().Be($"0,12 €");
        }

        [Theory]
        [InlineData(0.126)]
        [InlineData(0.127)]
        [InlineData(0.128)]
        public void Euros_ShouldCreateANewEURInstanceRoundingToCentsRoudingUpTheLastDigit_WhenTheLastDigitIsGreaterThanFive(decimal amount)
        {
            // Arrange
            Money sut = Money.Euros(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(0.13M);
            sut.Currency.Should().Be(Currency.EUR);
            sut.ToString().Should().Be($"0,13 €");
        }

        [Fact]
        public void Euros_ShouldCreateANewEURInstanceRoundingToCentsRoudingUpTheLastDigit_WhenTheLastDigitIsEqualThanFive()
        {
            // Arrange
            Money sut = Money.Euros(0.125M);

            // Act

            // Assert
            sut.Amount.Should().Be(0.13M);
            sut.Currency.Should().Be(Currency.EUR);
            sut.ToString().Should().Be($"0,13 €");
        }

        [Fact]
        public void Euros_ShouldHaveTwoDecimalPlaces()
        {
            // Arrange
            Money sut = Money.Euros(0);

            // Act

            // Assert
            sut.DecimalPlaces.Should().Be(2);
        }

        [Fact]
        public void Euros_ShouldHaveOneCentAsSmallestValue()
        {
            // Arrange
            Money sut = Money.Euros(0);

            // Act

            // Assert
            sut.SmallestAmount.Should().Be(0.01m);
        }        
    }
}
