using RJSilvas.MoneyLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Globalization;

namespace RJSilvas.MoneyLib.Tests
{
    public class MoneyTests
    {
        [Theory]
        [InlineData(0.12, 0.12, 0.24)]
        [InlineData(0.12, -0.12, 0)]
        [InlineData(0, -0.12, -0.12)]
        public void AddOperator_ShouldAddTwoAmountOfMoney(decimal amount1, decimal amount2, decimal expectedAmount)
        {
            // Arrange
            Money a = Money.Create(amount1, Currency.USD);
            Money b = Money.Create(amount2, Currency.USD);

            // Act
            var result = a + b;

            // Assert
            result.Amount.Should().Be(expectedAmount);
            result.Currency.Should().Be(Currency.USD);
        }

        [Fact]
        public void AddOperator_ShouldThrownAnException_WhenFirstValueIsNull()
        {
            // Arrange
            Money a = Money.Create(1, Currency.USD);
            string expectedMessage = "Money cannot be null";
            string actualMessage = string.Empty;

            // Act
            try
            {
                var result = null + a;
                Assert.True(false);
            }
            catch (MoneyNullException ex)
            {
                actualMessage = ex.Message;
                Assert.True(true);
            }

            // Assert
            actualMessage.Should().Be(expectedMessage);
        }

        [Fact]
        public void AddOperator_ShouldThrownAnException_WhenSecondValueIsNull()
        {
            // Arrange
            Money a = Money.Create(1, Currency.USD);
            Money b = null;
            string expectedMessage = "Money cannot be null";
            string actualMessage = string.Empty;

            // Act
            try
            {
                var result = a + b;
                Assert.True(false);
            }
            catch (MoneyNullException ex)
            {
                actualMessage = ex.Message;
                Assert.True(true);
            }

            // Assert
            actualMessage.Should().Be(expectedMessage);
        }

        [Fact]
        public void AddOperator_ShouldThrownAnException_WhenCurrenciesAreDifferent()
        {
            // Arrange
            Money a = Money.Create(1, Currency.USD);
            Money b = Money.Create(1, Currency.BRL);
            string expectedMessage = "Cannot perform operation between USD and BRL";
            string actualMessage = string.Empty;

            // Act
            try
            {
                var result = a + b;
                Assert.True(false);
            }
            catch (DifferentCurrencyException ex)
            {
                actualMessage = ex.Message;
                Assert.True(true);
            }

            // Assert
            actualMessage.Should().Be(expectedMessage);
        }

        [Theory]
        [InlineData(0.12, 0.12, 0)]
        [InlineData(0.12, -0.12, 0.24)]
        [InlineData(0, -0.12, 0.12)]
        public void SubtractOperator_ShouldAddTwoAmountOfReais(decimal amount1, decimal amount2, decimal expectedAmount)
        {
            // Arrange
            Money a = Money.Create(amount1, Currency.BRL);
            Money b = Money.Create(amount2, Currency.BRL);

            // Act
            var result = a - b;

            // Assert
            result.Amount.Should().Be(expectedAmount);
            result.Currency.Should().Be(Currency.BRL);
        }

        [Theory]
        [InlineData(-0.12)]
        [InlineData(0.12)]
        public void MinusOperator_ShouldNegateAmount(decimal amount)
        {
            // Arrange
            Money a = Money.Create(amount, Currency.BRL);

            // Act
            var result = -a;

            // Assert
            result.Amount.Should().Be(-amount);
            result.Currency.Should().Be(Currency.BRL);
        }

        [Fact]
        public void SubtractOperator_ShouldThrownAnException_WhenFirstValueIsNull()
        {
            // Arrange
            Money a = Money.Create(1, Currency.USD);
            string expectedMessage = "Money cannot be null";
            string actualMessage = string.Empty;

            // Act
            try
            {
                var result = null - a;
                Assert.True(false);
            }
            catch (MoneyNullException ex)
            {
                actualMessage = ex.Message;
                Assert.True(true);
            }

            // Assert
            actualMessage.Should().Be(expectedMessage);
        }

        [Fact]
        public void SubtractOperator_ShouldThrownAnException_WhenCurrenciesAreDifferent()
        {
            // Arrange
            Money a = Money.Create(1, Currency.USD);
            Money b = Money.Create(1, Currency.BRL);
            string expectedMessage = "Cannot perform operation between USD and BRL";
            string actualMessage = string.Empty;

            // Act
            try
            {
                var result = a - b;
                Assert.True(false);
            }
            catch (DifferentCurrencyException ex)
            {
                actualMessage = ex.Message;
                Assert.True(true);
            }

            // Assert
            actualMessage.Should().Be(expectedMessage);
        }

        [Fact]
        public void EqualOperator_ShouldReturnTrue_WhenTwoReaisAreEqual()
        {
            // Arrange
            Money a = Money.Create(1, Currency.BRL);
            Money b = Money.Create(1, Currency.BRL);

            // Act
            var result = a == b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void EqualOperator_ShouldReturnFalse_WhenTwoCurrenciesAreNotEqual()
        {
            // Arrange
            Money a = Money.Create(1, Currency.BRL);
            Money b = Money.Create(1, Currency.USD);

            // Act
            var result = a == b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualOperator_ShouldReturnFalse_WhenTwoReaisAreNotEqual()
        {
            // Arrange
            Money a = Money.Create(1, Currency.BRL);
            Money b = Money.Create(2, Currency.BRL);

            // Act
            var result = a == b;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InequalOperator_ShouldReturnTrue_WhenTwoReaisAreNotEqual()
        {
            // Arrange
            Money a = Money.Create(1, Currency.BRL);
            Money b = Money.Create(2, Currency.BRL);

            // Act
            var result = a != b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InequalOperator_ShouldReturnTrue_WhenCurrenciesAreNotEqual()
        {
            // Arrange
            Money a = Money.Create(1, Currency.BRL);
            Money b = Money.Create(1, Currency.USD);

            // Act
            var result = a != b;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InequalOperator_ShouldReturnFalse_WhenTwoReaisAreEqual()
        {
            // Arrange
            Money a = Money.Create(1, Currency.BRL);
            Money b = Money.Create(1, Currency.BRL);

            // Act
            var result = a != b;

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(1.044, 1, 1.044)]
        [InlineData(1.045, 1, 1.045)]
        [InlineData(1.046, 1, 1.046)]
        public void MultiplyOperator_ShouldMultiplyAScalarWithAMoney(decimal scalar, decimal amount, decimal expectedAmount)
        {
            // Arrange
            Money a = Money.Create(amount, Currency.BRL);

            // Act
            var result = scalar * a;

            // Assert
            result.Amount.Should().Be(expectedAmount);
            result.Currency.Should().Be(Currency.BRL);
        }

        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(1.044, 1, 1.044)]
        [InlineData(1.045, 1, 1.045)]
        [InlineData(1.046, 1, 1.046)]
        public void MultiplyOperator_ShouldMultiplyAMoneyWithScalar(decimal scalar, decimal amount, decimal expectedAmount)
        {
            // Arrange
            Money a = Money.Create(amount, Currency.BRL);

            // Act
            var result = a * scalar;

            // Assert
            result.Amount.Should().Be(expectedAmount);
            result.Currency.Should().Be(Currency.BRL);
        }

        [Fact]
        public void GetHashCode_ShouldReturnTrue_WhenTwoMoneysAreEqual()
        {
            // Arrange
            var one_reais_1 = Money.Create(1, Currency.BRL);
            var one_reais_2 = Money.Create(1, Currency.BRL);

            // Act


            // Assert
            one_reais_1.GetHashCode().Should().Be(one_reais_2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldReturnFalse_WhenTwoMoneysAreNotEqual()
        {
            // Arrange
            var one_reais_1 = Money.Create(1, Currency.BRL);
            var one_reais_2 = Money.Create(2, Currency.BRL);

            // Act


            // Assert
            one_reais_1.GetHashCode().Should().NotBe(one_reais_2.GetHashCode());
        }
    }
}
