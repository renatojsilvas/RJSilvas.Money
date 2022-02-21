using FluentAssertions;
using RJSilvas.MoneyLib.Core;
using System.Globalization;
using Xunit;

namespace RJSilvas.MoneyLib.Tests
{
    public class CurrencyTests
    {
        [Fact]
        public void BRL_ShouldReturnABrazilianRealInstance()
        {
            // Arrange

            // Act
            var sut = Currency.BRL;

            // Assert
            sut.IsActive.Should().BeTrue();
            sut.IsOfficialIso4217.Should().BeTrue();
            sut.Code.Should().Be("BRL");
            sut.Number.Should().Be(986);
            sut.Decimals.Should().Be(2);
            sut.Name.Should().Be("Brazilian Real");
            sut.Symbol.Should().Be("R$");
            sut.SmallestValue.Should().Be(0.01m);
            sut.CultureInfo.Should().Be(new CultureInfo("pt-BR"));
        }

        [Fact]
        public void USD_ShouldReturnAnUnitedStateDollarInstance()
        {
            // Arrange

            // Act
            var sut = Currency.USD;

            // Assert
            sut.IsActive.Should().BeTrue();
            sut.IsOfficialIso4217.Should().BeTrue();
            sut.Code.Should().Be("USD");
            sut.Number.Should().Be(840);
            sut.Decimals.Should().Be(2);
            sut.Name.Should().Be("US Dollar");
            sut.Symbol.Should().Be("$");
            sut.SmallestValue.Should().Be(0.01m);
            sut.CultureInfo.Should().Be(new CultureInfo("en-US"));
        }

        [Fact]
        public void USD_ShouldReturnABitcoinInstance()
        {
            // Arrange

            // Act
            var sut = Currency.BTC;

            // Assert
            sut.IsActive.Should().BeTrue();
            sut.IsOfficialIso4217.Should().BeFalse();
            sut.Code.Should().Be("BTC");
            sut.Number.Should().Be(null);
            sut.Decimals.Should().Be(8);
            sut.Name.Should().Be("Bitcoin");
            sut.Symbol.Should().Be("₿");
            sut.SmallestValue.Should().Be(0.00000001m);
            sut.CultureInfo.Should().Be(new CultureInfo("en-US"));
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenTwoCurrenciesAreEqual()
        {
            // Arrange
            var real = Currency.BRL;
            var another_real = Currency.BRL;

            // Act
            var sut = real.Equals(another_real);

            // Assert
            sut.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenTwoCurrenciesAreNotEqual()
        {
            // Arrange
            var real = Currency.BRL;
            var dollar = Currency.USD;

            // Act
            var sut = real.Equals(dollar);

            // Assert
            sut.Should().BeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenTwoCurrenciesAreComparedToMoney()
        {
            // Arrange
            var real = Currency.BRL;
            var one_real = Money.Create(1, Currency.BRL);

            // Act
            var sut = real.Equals(one_real);

            // Assert
            sut.Should().BeFalse();
        }
    }
}
