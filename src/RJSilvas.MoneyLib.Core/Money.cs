using System;
using System.Collections.Generic;
using System.Globalization;

namespace RJSilvas.MoneyLib.Core
{
    public partial class Money
    {
        public static Money Reais(decimal amount)
        {
            return Create(amount, Currency.BRL, 0.01m, new CultureInfo("pt-BR"));
        }

        public static Money Dollars(decimal amount)
        {
            return Create(amount, Currency.USD, 0.01m, new CultureInfo("en-US"));
        }

        public static Money Euros(decimal amount)
        {
            return Create(amount, Currency.EUR, 0.01m, new CultureInfo("fr-FR"));
        }

        public static Money Bitcoins(decimal amount)
        {
            CultureInfo ci = new CultureInfo("en-US");
            ci.NumberFormat.CurrencySymbol = "₿";
            return Create(amount, Currency.BTC, 0.00000001m, ci);
        }

        public static Money Create(decimal amount, Currency currency, decimal smallestAmount,
            CultureInfo cultureInfo, MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            var decimalPlaces = (int)(-Math.Log10(decimal.ToDouble(Math.Abs(smallestAmount))));
            var roundedAmount = Math.Round(amount, decimalPlaces, rounding);
            return new Money(roundedAmount, currency, smallestAmount, decimalPlaces, cultureInfo);
        }

        private readonly CultureInfo cultureInfo;

        private Money(decimal amount, Currency currency, decimal smallestAmount,
                      int decimalPlaces, CultureInfo cultureInfo)
        {
            Amount = amount;
            Currency = currency;
            SmallestAmount = smallestAmount;            
            DecimalPlaces = decimalPlaces;
            this.cultureInfo = cultureInfo;
        }

        public decimal Amount { get; }
        public Currency Currency { get; }
        public int DecimalPlaces { get; }
        public decimal SmallestAmount { get; }

        public override string ToString()
        {
            return Amount.ToString($"C{DecimalPlaces}", cultureInfo);
        }

        public override bool Equals(object obj)
        {
            return obj is Money money &&
                   Amount == money.Amount &&
                   Currency == money.Currency &&
                   DecimalPlaces == money.DecimalPlaces &&
                   SmallestAmount == money.SmallestAmount;
        }

        public static Money operator+(Money amount1, Money amount2)
        {
            if (amount1 is null || amount2 is null)
                throw new MoneyNullException();

            if (amount1.Currency != amount2.Currency)
                throw new DifferentCurrencyException(amount1, amount2);

            return new Money(amount1.Amount + amount2.Amount,
                amount1.Currency, amount1.SmallestAmount,
                amount1.DecimalPlaces, amount1.cultureInfo);
        }

        public static Money operator -(Money amount1, Money amount2)
        {
            return amount1 + (-amount2);
        }

        public static Money operator -(Money amount)
        {
            return new Money(-amount.Amount,
                amount.Currency, amount.SmallestAmount,
                amount.DecimalPlaces, amount.cultureInfo);
        }

        public static Money operator *(decimal scalar, Money amount)
        {
            return new Money(scalar * amount.Amount,
                amount.Currency, amount.SmallestAmount,
                amount.DecimalPlaces, amount.cultureInfo);
        }

        public static Money operator *(Money amount, decimal scalar)
        {
            return new Money(scalar * amount.Amount,
                amount.Currency, amount.SmallestAmount,
                amount.DecimalPlaces, amount.cultureInfo);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency, DecimalPlaces, SmallestAmount);
        }

        public static bool operator ==(Money left, Money right)
        {
            return EqualityComparer<Money>.Default.Equals(left, right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !(left == right);
        }
    }
}
