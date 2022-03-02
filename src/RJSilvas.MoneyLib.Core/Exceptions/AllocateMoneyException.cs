using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJSilvas.MoneyLib.Core
{
    public class AllocateMoneyException : Exception
    {
        public AllocateMoneyException(int parts)
            : base($"Money cannot be allocated in {parts} parts")
        {
        }
    }
}
