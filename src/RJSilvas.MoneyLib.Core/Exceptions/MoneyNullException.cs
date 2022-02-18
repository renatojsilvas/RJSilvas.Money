using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJSilvas.MoneyLib.Core
{
    public class MoneyNullException : Exception
    {
        public override string Message => "Money cannot be null";
    }

    
}
