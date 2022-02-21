using System;
using System.Collections.Generic;
using System.Globalization;

namespace RJSilvas.MoneyLib.Core
{
    public class Currency
    {
        public static Currency BRL => new(true, true, 986, new CultureInfo("pt-BR"));
        public static Currency USD => new(true, true, 840, new CultureInfo("en-US"));
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

        public bool IsActive { get; }
        public bool IsOfficialIso4217 { get; }
        public string Code { get;  }
        public uint? Number { get;  }
        public int Decimals { get; }
        public string Name { get; }
        public string Symbol { get; }
        public decimal SmallestValue { get; }
        public CultureInfo CultureInfo { get; }

        public override bool Equals(object obj)
        {
            return obj is Currency currency &&
                   EqualityComparer<RegionInfo>.Default.Equals(regionInfo, currency.regionInfo) &&
                   IsActive == currency.IsActive &&
                   IsOfficialIso4217 == currency.IsOfficialIso4217 &&
                   Code == currency.Code &&
                   Number == currency.Number &&
                   Decimals == currency.Decimals &&
                   Name == currency.Name &&
                   Symbol == currency.Symbol &&
                   SmallestValue == currency.SmallestValue &&
                   EqualityComparer<CultureInfo>.Default.Equals(CultureInfo, currency.CultureInfo);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(regionInfo);
            hash.Add(IsActive);
            hash.Add(IsOfficialIso4217);
            hash.Add(Code);
            hash.Add(Number);
            hash.Add(Decimals);
            hash.Add(Name);
            hash.Add(Symbol);
            hash.Add(SmallestValue);
            hash.Add(CultureInfo);
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
