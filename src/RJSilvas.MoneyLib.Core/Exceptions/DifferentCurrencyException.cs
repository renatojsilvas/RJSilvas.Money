using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJSilvas.MoneyLib.Core
{
    public class DifferentCurrencyException : Exception
    {
        private Money amount1;
        private Money amount2;

        public DifferentCurrencyException(Money amount1, Money amount2)
        {
            this.amount1 = amount1;
            this.amount2 = amount2;
        }

        public override string Message => $"Cannot perform operation between {amount1.Currency} and {amount2.Currency}";
    }
}
