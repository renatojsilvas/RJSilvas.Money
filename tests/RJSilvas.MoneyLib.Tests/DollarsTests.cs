using RJSilvas.MoneyLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace RJSilvas.MoneyLib.Tests
{
    [UseCulture("pt-BR")]
    public class DollarsTests
    {
        [Theory]       
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public void Dollars_ShouldCreateANewUSDInstance_WhenAmountIsGreaterThanZero(decimal amount)
        {
            // Arrange
            Money sut = Money.Dollars(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(amount);
            sut.Currency.Should().Be(Currency.USD);
            sut.ToString().Should().Be($"${amount}.00");
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-10)]
        [InlineData(-15)]
        public void Dollars_ShouldCreateANewUSDInstance_WhenAmountIsLessThanZero(decimal amount)
        {
            // Arrange
            Money sut = Money.Dollars(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(amount);
            sut.Currency.Should().Be(Currency.USD);
            sut.ToString().Should().Be($"-${-amount}.00");
        }

        [Fact]
        public void Dollars_ShouldCreateANewUSDInstance_WhenAmountIsZero()
        {
            // Arrange
            Money sut = Money.Dollars(0);

            // Act

            // Assert
            sut.Amount.Should().Be(0);
            sut.Currency.Should().Be(Currency.USD);
            sut.ToString().Should().Be($"$0.00");
        }

        [Theory]
        [InlineData(0.12)]
        [InlineData(0.123)]
        [InlineData(0.124)]
        [InlineData(0.12409)]
        public void Dollars_ShouldCreateANewUSDInstanceRoundingToCentsKeepingTheLastDigit_WhenTheLastDigitIsLowerThanFive(decimal amount)
        {
            // Arrange
            Money sut = Money.Dollars(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(0.12M);
            sut.Currency.Should().Be(Currency.USD);
            sut.ToString().Should().Be($"$0.12");
        }

        [Theory]
        [InlineData(0.126)]
        [InlineData(0.127)]
        [InlineData(0.128)]
        public void Dollars_ShouldCreateANewUSDInstanceRoundingToCentsRoudingUpTheLastDigit_WhenTheLastDigitIsGreaterThanFive(decimal amount)
        {
            // Arrange
            Money sut = Money.Dollars(amount);

            // Act

            // Assert
            sut.Amount.Should().Be(0.13M);
            sut.Currency.Should().Be(Currency.USD);
            sut.ToString().Should().Be($"$0.13");
        }

        [Fact]
        public void Dollars_ShouldCreateANewUSDInstanceRoundingToCentsRoudingUpTheLastDigit_WhenTheLastDigitIsEqualThanFive()
        {
            // Arrange
            Money sut = Money.Dollars(0.125M);

            // Act

            // Assert
            sut.Amount.Should().Be(0.13M);
            sut.Currency.Should().Be(Currency.USD);
            sut.ToString().Should().Be($"$0.13");
        }

        [Fact]
        public void Dollars_ShouldHaveTwoDecimalPlaces()
        {
            // Arrange
            Money sut = Money.Dollars(0);

            // Act

            // Assert
            sut.DecimalPlaces.Should().Be(2);
        }

        [Fact]
        public void Dollars_ShouldHaveOneCentAsSmallestValue()
        {
            // Arrange
            Money sut = Money.Dollars(0);

            // Act

            // Assert
            sut.SmallestAmount.Should().Be(0.01m);
        }
    }
}
