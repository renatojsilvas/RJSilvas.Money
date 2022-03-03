using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJSilvas.MoneyLib.Core
{
    public interface IExchangeRateRepository
    {
        void Register(Currency from, Currency to, ExchangeRate rate);
        ExchangeRate GetRate(Currency from, Currency to); 
    }
}
