using RJSilvas.MoneyLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RJSilvas.MoneyLib.Tests
{
    public class CurrencyConverterServiceTests
    {
        [Fact]
        public void ConvertTo_ShouldConvertMoneyToAnotherCurrencyByAnExchangeRate()
        {
            // Arrange

            var currencyConverterService = new CurrencyConverterService(exchangeRateRepository);
            var one_real = Money.Create(1, Currency.BRL);
            var one_real_in_dollar = Money.Create(5.20m, Currency.USD);

            // Act
            var result = currencyConverterService.ConvertTo(one_real, Currency.USD);

            // Assert
            result.Should().Be(one_real_in_dollar);
        }
    }
}
