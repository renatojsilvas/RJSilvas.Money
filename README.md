# RJSilvas.Money
Library that encapsulates the money information

## Installation

Use the [Nuget Package Manager](https://www.nuget.org/) to install RJSilvas.Money.

```powershell
Install-Package RJSilvas.MoneyLib.Core
```

## Introduction

Money library that encapsulates all money behaviour such as formatting, converting, rounding and arithmetic.

### Features

- Creating a money directly as Money.Reais(5), Money.Dollars(1) or Money.Bitcoins(10);
- Formatting a money value to a string by .ToString(). The default culture is pt-BR for BRL, en-US for USD and BTC;
- Rounding to the smallest quantity of the currency;
- Doing arithmetic by + and - operations. Besides, it is possible to multiply a money quantity to a scalar.


## Usage

```csharp
using System;
using RJSilvas.MoneyLib.Core;

namespace RJSilvas.MoneyLib.Core.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var one_real = Money.Reais(1); // Create 1 BRL
            Console.WriteLine(one_real); // R$ 1,00
            
            var two_reais = Money.Reais(2); // Create 2 BRL
            Console.WriteLine(two_reais); // R$ 2,00
            
            var three_reais = Money.Reais(3); // Create 3 BRL
            Console.WriteLine(three_reais); // R$ 3,00           
           
            // Sum two amount of moneys
            Console.WriteLine(one_real + three_reais); // R$ 4,00
            
            // Subtract two amount of moneys
            Console.WriteLine(three_reais - one_real); // R$ 2,00
            
            // Multiply a scaler to money
            Console.WriteLine(2 * two_reais); // R$ 4,00
            
            // Multiply a money to scalar
            Console.WriteLine(two_reais * 2); // R$ 4,00
            
            // Samallest quantity of the money
            Console.WriteLine(two_reais); // R$ 0,01
        }
    }
}
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
