using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJSilvas.MoneyLib.Core
{
    public class ExchangeRate
    {
        public Currency Source { get; }

        public Currency Destination { get; }
        public decimal Value { get; }
    }
}
