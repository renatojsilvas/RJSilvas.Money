using System;
using System.Collections.Generic;

namespace RJSilvas.MoneyLib.Core
{
    public partial class Money
    {
        public static Money Reais(decimal amount)
        {
            return Create(amount, Currency.BRL);
        }

        public static Money Dollars(decimal amount)
        {
            return Create(amount, Currency.USD);
        }

        public static Money Bitcoins(decimal amount)
        {
            return Create(amount, Currency.BTC);
        }

        public static Money Create(decimal amount, Currency currency, MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            var roundedAmount = Math.Round(amount, currency.Decimals, rounding);
            return new Money(roundedAmount, currency, rounding);
        }

        private readonly MidpointRounding rounding;

        private Money(decimal amount, Currency currency, MidpointRounding rounding)
        {
            Amount = amount;
            Currency = currency;
            this.rounding = rounding;
        }

        public decimal Amount { get; }
        public Currency Currency { get; }
        public int DecimalPlaces => Currency.Decimals;
        public decimal SmallestAmount => Currency.SmallestValue;

        public override string ToString()
        {
            return Amount.ToString($"C{DecimalPlaces}", Currency.CultureInfo);
        }

        public override bool Equals(object obj)
        {
            return obj is Money money &&
                   Amount == money.Amount &&
                   Currency == money.Currency;
        }

        public static Money operator+(Money amount1, Money amount2)
        {
            if (amount1 is null || amount2 is null)
                throw new MoneyNullException();

            if (amount1.Currency != amount2.Currency)
                throw new DifferentCurrencyException(amount1, amount2);

            return new Money(amount1.Amount + amount2.Amount,
                amount1.Currency,
                amount1.rounding);
        }

        public static Money operator -(Money amount1, Money amount2)
        {
            return amount1 + (-amount2);
        }

        public static Money operator -(Money amount)
        {
            return new Money(-amount.Amount,
                amount.Currency,
                amount.rounding);
        }

        public static Money operator *(decimal scalar, Money amount)
        {
            return new Money(scalar * amount.Amount,
                amount.Currency,
                amount.rounding);
        }

        public static Money operator *(Money amount, decimal scalar)
        {
            return new Money(scalar * amount.Amount,
                amount.Currency,
                amount.rounding);
        }

        public static Money operator *(Money money, Percent percentage)
        {
            return new Money(Math.Round(money.Amount * percentage.FractionalValue,
                                        money.DecimalPlaces, MidpointRounding.ToZero),
                money.Currency,
                money.rounding);
        }

        public static Money operator +(Money money, Percent percentage)
        {
            return new Money((money + (money * percentage)).Amount,
                money.Currency,
                money.rounding);
        }

        public static Money operator -(Money money, Percent percentage)
        {
            return new Money((money - (money * percentage)).Amount,
                money.Currency,
                money.rounding);
        }

        public static Percent operator /(Money money1, Money money2)
        {
            return Percent.FromFraction(money1.Amount / money2.Amount);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency, DecimalPlaces);
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
