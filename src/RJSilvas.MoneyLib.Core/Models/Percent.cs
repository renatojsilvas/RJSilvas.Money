﻿using System;
using System.Collections.Generic;

namespace RJSilvas.MoneyLib.Core
{
    public class Percent
    {
        /// <summary>
        /// Create a percent instance from the percent value, i.e. 100 for 100%. The amountOfPercentage input value
        /// is rounded with 5 decimal places
        /// </summary>
        /// <param name="amountOfPercentage"></param>
        /// <returns>Percent Instance</returns>       
        public static Percent FromValue(decimal amountOfPercentage)
        {
            return new Percent(Math.Round(amountOfPercentage, 5, MidpointRounding.AwayFromZero));
        }

        /// <summary>
        /// Create a percent instance from the fractional value, i.e. 1 for 100%
        /// </summary>
        /// <param name="amountOfPercentage"></param>
        /// <returns>Percent Instance</returns>
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

        /// <summary>
        /// Percentage Value. When the percentage is 100%, Percentage is 100m
        /// </summary>
        public decimal Percentage { get; }

        /// <summary>
        /// Fractional Value. When the percentage is 100%, FractionalValue is 1m
        /// </summary>
        public decimal FractionalValue => Percentage / 100;

        public static bool operator ==(Percent left, Percent right)
        {
            return EqualityComparer<Percent>.Default.Equals(left, right);
        }

        public static bool operator !=(Percent left, Percent right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns percentage as string with 5 decimal places. The decimal separator depending on current culture.
        /// When 100% and culture is pt-BR, returns 100,00000 %
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Percentage:F5} %";
        }

        /// <summary>
        /// Return percentage as string with customizable decimal places. The decimal separator depending on current culture.
        /// When 100%, culture is pt-BR and decimalPlaces is 1, returns 100,0 %
        /// </summary>
        /// <param name="decimalPlaces"></param>
        /// <returns>Percentage with customizable number of decimal places</returns>
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
