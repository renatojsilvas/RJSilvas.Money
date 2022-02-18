using FluentAssertions;
using RJSilvas.MoneyLib.Core;
using Xunit;

namespace RJSilvas.MoneyLib.Tests
{
    [UseCulture("pt-BR")]
    public class BitcoinTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public void Bitcoins_ShouldCreateANewBTCInstance_WhenAmountIsGreaterThanZero(decimal amount)
        {
            // Arrange
            Money sut = Money.Bitcoins(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(amount);
            sut.Currency.Should().Be(Currency.BTC);
            sut.ToString().Should().Be($"₿{amount}.00000000");
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-10)]
        [InlineData(-15)]
        public void Bitcoins_ShouldCreateANewBTCInstance_WhenAmountIsLessThanZero(decimal amount)
        {
            // Arrange
            Money sut = Money.Bitcoins(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(amount);
            sut.Currency.Should().Be(Currency.BTC);
            sut.ToString().Should().Be($"-₿{-amount}.00000000");
        }

        [Fact]
        public void Bitcoins_ShouldCreateANewBTCInstance_WhenAmountIsZero()
        {
            // Arrange
            Money sut = Money.Bitcoins(0);

            // Act

            // Assert
            sut.Amount.Should().Be(0);
            sut.Currency.Should().Be(Currency.BTC);
            sut.ToString().Should().Be($"₿0.00000000");
        }

        [Theory]
        [InlineData(0.123456781)]
        [InlineData(0.123456782)]
        [InlineData(0.123456783)]
        [InlineData(0.123456784)]
        public void Bitcoins_ShouldCreateANewBTCInstanceRoundingToCentsKeepingTheLastDigit_WhenTheLastDigitIsLowerThanFive(decimal amount)
        {
            // Arrange
            Money sut = Money.Bitcoins(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(0.12345678M);
            sut.Currency.Should().Be(Currency.BTC);
            sut.ToString().Should().Be($"₿0.12345678");
        }

        [Theory]
        [InlineData(0.123456786)]
        [InlineData(0.123456787)]
        [InlineData(0.123456788)]
        [InlineData(0.123456789)]
        public void Bitcoins_ShouldCreateANewBTCInstanceRoundingToCentsRoudingUpTheLastDigit_WhenTheLastDigitIsGreaterThanFive(decimal amount)
        {
            // Arrange
            Money sut = Money.Bitcoins(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(0.12345679M);
            sut.Currency.Should().Be(Currency.BTC);
            sut.ToString().Should().Be($"₿0.12345679");
        }

        [Fact]
        public void Bitcoins_ShouldCreateANewBTCInstanceRoundingToCentsRoudingUpTheLastDigit_WhenTheLastDigitIsEqualThanFive()
        {
            // Arrange
            Money sut = Money.Bitcoins(0.123456785M);

            // Act

            // Assert
            sut.Amount.Should().Be(0.12345679M);
            sut.Currency.Should().Be(Currency.BTC);
            sut.ToString().Should().Be($"₿0.12345679");
        }

        [Fact]
        public void Bitcoins_ShouldHaveEightDecimalPlaces()
        {
            // Arrange
            Money sut = Money.Bitcoins(0);

            // Act

            // Assert
            sut.DecimalPlaces.Should().Be(8);
        }

        [Fact]
        public void Bitcoins_ShouldHaveOneDSatoshiAsSmallestValue()
        {
            // Arrange
            Money sut = Money.Bitcoins(0);

            // Act

            // Assert
            sut.SmallestAmount.Should().Be(0.00000001m);
        }
    }
}
