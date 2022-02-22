using System;

namespace RJSilvas.MoneyLib.Core
{
    /// <summary>
    /// Excpetion raised when two currencies are different
    /// </summary>
    public class DifferentCurrencyException : Exception
    {
        private readonly Money amount1;
        private readonly Money amount2;

        public DifferentCurrencyException(Money amount1, Money amount2)
        {
            this.amount1 = amount1;
            this.amount2 = amount2;
        }

        public override string Message => $"Cannot perform operation between {amount1.Currency.Code} and {amount2.Currency.Code}";
    }
}
