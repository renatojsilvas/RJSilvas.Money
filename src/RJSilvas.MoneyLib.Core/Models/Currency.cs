using System;
using System.Collections.Generic;
using System.Globalization;

namespace RJSilvas.MoneyLib.Core
{
    public class Currency
    {
        /// <summary>
        /// Returns a BRL instance
        /// </summary>
        public static Currency BRL => new(true, true, 986, new CultureInfo("pt-BR"));
        
        /// <summary>
        /// Returns a USD instance
        /// </summary>
        public static Currency USD => new(true, true, 840, new CultureInfo("en-US"));

        /// <summary>
        /// Returns a BTC instance
        /// </summary>
        public static Currency BTC => new(true, false, "BTC", 8, "Bitcoin", "₿");

        private RegionInfo regionInfo;

        private Currency(bool isActive, bool isOfficialIso4217, uint? number, CultureInfo cultureInfo)
        {
            IsActive = isActive;
            IsOfficialIso4217 = isOfficialIso4217;
            Number = number;
            CultureInfo = cultureInfo;

            regionInfo = new RegionInfo(cultureInfo.Name);
            
            Code = regionInfo.ISOCurrencySymbol;
            Decimals = cultureInfo.NumberFormat.CurrencyDecimalDigits;
            Name = regionInfo.CurrencyEnglishName;
            Symbol = regionInfo.CurrencySymbol;
            SmallestValue = (decimal)Math.Pow(10, -Decimals);
        }

        private Currency(bool isActive, bool isOfficialIso4217, string code, int decimals, string name, string symbol, CultureInfo cultureInfo = null)
        {
            IsActive = isActive;
            IsOfficialIso4217 = isOfficialIso4217;
            Number = null;

            if (cultureInfo == null)
            {
                CultureInfo ci = new CultureInfo("en-US");
                ci.NumberFormat.CurrencySymbol = symbol;
                CultureInfo = ci;
            }

            Code = code;
            Decimals = decimals;
            Name = name;
            Symbol = symbol;            
            SmallestValue = (decimal)Math.Pow(10, -Decimals);
        }

        /// <summary>
        /// Returns whether the currency is active
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        /// Returns if the currency is official ISO4217
        /// </summary>
        public bool IsOfficialIso4217 { get; }

        /// <summary>
        /// Returns the code of the currency
        /// </summary>
        public string Code { get;  }

        /// <summary>
        /// Returns the code number of the currency. Non-official currencies do not have code number 
        /// </summary>
        public uint? Number { get;  }

        /// <summary>
        /// Returns the number of decimal places of the currency
        /// </summary>
        public int Decimals { get; }

        /// <summary>
        /// Returns the name in english
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Returns the symbol of the currency
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Returns the smallest amount of the currency
        /// </summary>
        public decimal SmallestValue { get; }

        /// <summary>
        /// Returns the culture of the currency
        /// </summary>
        public CultureInfo CultureInfo { get; }

        public override bool Equals(object obj)
        {
            return obj is Currency currency &&
                   Code == currency.Code;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Code);
            return hash.ToHashCode();
        }

        public static bool operator ==(Currency left, Currency right)
        {
            return EqualityComparer<Currency>.Default.Equals(left, right);
        }

        public static bool operator !=(Currency left, Currency right)
        {
            return !(left == right);
        }
    }
}
