using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJSilvas.MoneyLib.Core
{
    /// <summary>
    /// Exception raised when the money is null
    /// </summary>
    public class MoneyNullException : Exception
    {
        public override string Message => "Money cannot be null";
    }

    
}
