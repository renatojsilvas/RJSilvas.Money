using FluentAssertions;
using RJSilvas.MoneyLib.Core;
using Xunit;

namespace RJSilvas.MoneyLib.Tests
{
    public class PercentTests
    {
        [Theory]
        [InlineData(100, 1, 1)]
        [InlineData(100, 2, 2)]
        [InlineData(100, 3, 3)]
        [InlineData(97.97, 1, 0.97)]
        [InlineData(123.46, 10, 12.34)]
        public void MoneyTimesPercent_ShouldCalculatePercentOfAmountOfMoney(decimal amountOfMoney, 
            decimal amountOfPercentage, decimal expectedAmountOfMoney)
        {
            // Arrange
            var money = Money.Reais(amountOfMoney);
            var percentage = Percent.FromValue(amountOfPercentage);
            var expected = Money.Reais(expectedAmountOfMoney);

            // Act
            var result = money * percentage;

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(100, 1, 101)]
        [InlineData(100, 2, 102)]
        [InlineData(100, 3, 103)]
        [InlineData(97.97, 1, 97.97 + 0.97)]
        public void MoneyPlusPercent_ShouldAddAnAmountOfMoneyThatIsAPercentage(decimal amountOfMoney, 
            decimal amountOfPercentage, decimal expectedAmountOfMoney)
        {
            // Arrange
            var money = Money.Reais(amountOfMoney);
            var percentage = Percent.FromValue(amountOfPercentage);
            var expected = Money.Reais(expectedAmountOfMoney);

            // Act
            var result = money + percentage;

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(100, 1, 99)]
        [InlineData(100, 2, 98)]
        [InlineData(100, 3, 97)]
        [InlineData(97.97, 1, 97.97 - 0.97)]
        public void MoneyMinusPercent_ShouldSubtractAnAmountOfMoneyThatIsAPercentage(decimal amountOfMoney, 
            decimal amountOfPercentage, decimal expectedAmountOfMoney)
        {
            // Arrange
            var money = Money.Reais(amountOfMoney);
            var percentage = Percent.FromValue(amountOfPercentage);
            var expected = Money.Reais(expectedAmountOfMoney);

            // Act
            var result = money - percentage;

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 100, 0)]
        [InlineData(1, 100,  1)]
        [InlineData(10.12, 97.97, 10.32969)]
        [InlineData(1.23, 97.97, 1.25549)]
        [InlineData(1.21, 97.97, 1.23507)]
        public void MoneyDivideMoney_ShouldResultInAPergentage(decimal amountOfMoney1, decimal amountOfMoney2, decimal percentageResult)
        {
            // Arrange
            var money1 = Money.Reais(amountOfMoney1);
            var money2 = Money.Reais(amountOfMoney2);
            var expected = Percent.FromValue(percentageResult);

            // Act
            var result = money1 / money2;

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        [UseCulture("pt-BR")]
        public void ToString_ShouldReturnPercentageWithFiveDecimalPlacesWithComma_WhenDefaultAndPtBR()
        {
            // Arrange
            var one_percent = Percent.FromValue(1);
            var expected = "1,00000 %";

            // Act
            var result = one_percent.ToString();

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        [UseCulture("en-US")]
        public void ToString_ShouldReturnPercentageWithFiveDecimalPlacesWithDot_WhenDefaultAndEnUS()
        {
            // Arrange
            var one_percent = Percent.FromValue(1);
            var expected = "1.00000 %";

            // Act
            var result = one_percent.ToString();

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        [UseCulture("pt-BR")]
        public void ToString_ShouldReturnPercentageWithTwoDecimalPlacesWithComma_WhenTwoAndPtBR()
        {
            // Arrange
            var one_percent = Percent.FromValue(1);
            var expected = "1,00 %";

            // Act
            var result = one_percent.ToString(2);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(0.01, 1)]
        [InlineData(0.01234567, 1.23457)]
        public void FromFraction_ShouldCreatePercentFromFraction(decimal fraction, decimal percentage)
        {
            // Arrange
            var one_percent = Percent.FromFraction(fraction);
            var expected = Percent.FromValue(percentage);

            // Act

            // Assert
            one_percent.Should().Be(expected);
        }

        [Fact]
        public void EqualOperator_ShouldReturnTrue_WhenTwoCurrenciesAreEqual()
        {
            // Arrange
            var one_percent_1 = Percent.FromValue(1);
            var one_percent_2 = Percent.FromValue(1);

            // Act
            var result = one_percent_1 == one_percent_2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenTwoCurrenciesAreEqual()
        {
            // Arrange
            var one_percent_1 = Percent.FromValue(1);
            var one_percent_2 = Percent.FromValue(1);

            // Act
            var result = one_percent_1.Equals(one_percent_2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenComparingTwoDifferentObjects()
        {
            // Arrange
            var one_percent_1 = Percent.FromValue(1);
            var one_reais = Money.Reais(1);

            // Act
            var result = one_percent_1.Equals(one_reais);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualOperator_ShouldReturnFalse_WhenTwoCurrenciesAreNotEqual()
        {
            // Arrange
            var one_percent_1 = Percent.FromValue(1);
            var one_percent_2 = Percent.FromValue(2);

            // Act
            var result = one_percent_1 == one_percent_2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void DifferentOperator_ShouldReturnFalse_WhenTwoCurrenciesAreEqual()
        {
            // Arrange
            var one_percent_1 = Percent.FromValue(1);
            var one_percent_2 = Percent.FromValue(1);

            // Act
            var result = one_percent_1 != one_percent_2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void DifferentOperator_ShouldReturnTrue_WhenTwoCurrenciesAreNotEqual()
        {
            // Arrange
            var one_percent_1 = Percent.FromValue(1);
            var one_percent_2 = Percent.FromValue(2);

            // Act
            var result = one_percent_1 != one_percent_2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_ShouldReturnTrue_WhenTwoCurrenciesAreEqual()
        {
            // Arrange
            var one_percent_1 = Percent.FromValue(1);
            var one_percent_2 = Percent.FromValue(1);

            // Act
            

            // Assert
            one_percent_1.GetHashCode().Should().Be(one_percent_2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldReturnFalse_WhenTwoCurrenciesAreNotEqual()
        {
            // Arrange
            var one_percent_1 = Percent.FromValue(1);
            var one_percent_2 = Percent.FromValue(2);

            // Act


            // Assert
            one_percent_1.GetHashCode().Should().NotBe(one_percent_2.GetHashCode());
        }
    }
}
