using System;
using System.Collections.Generic;

namespace RJSilvas.MoneyLib.Core
{
    public class Percent
    {
        public static Percent FromValue(decimal amountOfPercentage)
        {
            return new Percent(Math.Round(amountOfPercentage, 5, MidpointRounding.AwayFromZero));
        }

        public static Percent FromFraction(decimal fraction)
        {
            return Percent.FromValue(fraction * 100);
        }

        private Percent(decimal value)
        {
            Percentage = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Percent percent &&
                   Percentage == percent.Percentage;
        }
        

        public decimal Percentage { get; }
        public decimal FractionalValue => Percentage / 100;

        public static bool operator ==(Percent left, Percent right)
        {
            return EqualityComparer<Percent>.Default.Equals(left, right);
        }

        public static bool operator !=(Percent left, Percent right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{Percentage:F5} %";
        }

        public string ToString(int decimalPlaces)
        {
            return Percentage.ToString($"F{decimalPlaces}") + " %";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Percentage, FractionalValue);
        }
    }
}
