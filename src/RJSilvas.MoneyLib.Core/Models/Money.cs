using System;
using System.Collections.Generic;
using System.Linq;

namespace RJSilvas.MoneyLib.Core
{
    public partial class Money
    {
        /// <summary>
        /// Create a BRL instance
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Money Reais(decimal amount)
        {
            return Create(amount, Currency.BRL);
        }

        /// <summary>
        /// Create a USD instance
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Money Dollars(decimal amount)
        {
            return Create(amount, Currency.USD);
        }

        /// <summary>
        /// Create a BTC instance
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Money Bitcoins(decimal amount)
        {
            return Create(amount, Currency.BTC);
        }

        /// <summary>
        /// Create a Money instance with a amount, currency and optionally the rounding method 
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="rounding"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Amount of money
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// Currency of money
        /// </summary>
        public Currency Currency { get; }

        /// <summary>
        /// Number of decimal places
        /// </summary>
        public int DecimalPlaces => Currency.Decimals;

        /// <summary>
        /// The smallest amount of a given currency. In case of USD the smallest amount is cents ($0.01)
        /// </summary>
        public decimal SmallestAmount => Currency.SmallestValue;

        /// <summary>
        /// Money representations as string, with symbol and correct number of decimal places
        /// </summary>
        /// <returns>Money as string</returns>
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

        /// <summary>
        /// Sum two moneys with the same currency.
        /// </summary>
        /// <param name="amount1"></param>
        /// <param name="amount2"></param>
        /// <returns>The sum of two moneys</returns>
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

        /// <summary>
        /// Subtract two moneys with the same currency.
        /// </summary>
        /// <param name="amount1"></param>
        /// <param name="amount2"></param>
        /// <returns>The subtraction of two moneys</returns>
        public static Money operator -(Money amount1, Money amount2)
        {
            return amount1 + (-amount2);
        }

        /// <summary>
        /// Negate a money
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Money negated</returns>
        public static Money operator -(Money amount)
        {
            return new Money(-amount.Amount,
                amount.Currency,
                amount.rounding);
        }

        /// <summary>
        /// Multiply a Money by a scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="amount"></param>
        /// <returns>Money multiplied by a scalar</returns>
        public static Money operator *(decimal scalar, Money amount)
        {
            return new Money(scalar * amount.Amount,
                amount.Currency,
                amount.rounding);
        }

        /// <summary>
        /// Multiply a Money by a scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="amount"></param>
        /// <returns>Money multiplied by a scalar</returns>
        public static Money operator *(Money amount, decimal scalar)
        {
            return new Money(scalar * amount.Amount,
                amount.Currency,
                amount.rounding);
        }

        /// <summary>
        /// Calculate a percentage of money
        /// </summary>
        /// <param name="money"></param>
        /// <param name="percentage"></param>
        /// <returns>Precentage of money</returns>
        public static Money operator *(Money money, Percent percentage)
        {
            return new Money(Math.Round(money.Amount * percentage.FractionalValue,
                                        money.DecimalPlaces, MidpointRounding.ToZero),
                money.Currency,
                money.rounding);
        }

        /// <summary>
        /// Add a percentage to a money
        /// </summary>
        /// <param name="money"></param>
        /// <param name="percentage"></param>
        /// <returns>Money added by a percentage</returns>
        public static Money operator +(Money money, Percent percentage)
        {
            return new Money((money + (money * percentage)).Amount,
                money.Currency,
                money.rounding);
        }

        /// <summary>
        /// Subtract a percentage from money
        /// </summary>
        /// <param name="money"></param>
        /// <param name="percentage"></param>
        /// <returns>Money subtracted by a percentage</returns>
        public static Money operator -(Money money, Percent percentage)
        {
            return new Money((money - (money * percentage)).Amount,
                money.Currency,
                money.rounding);
        }

        /// <summary>
        /// Calculate the ratio in percent of two moneys
        /// </summary>
        /// <param name="money1"></param>
        /// <param name="money2"></param>
        /// <returns>The ratio in percent</returns>
        public static Percent operator /(Money money1, Money money2)
        {
            return Percent.FromFraction(money1.Amount / money2.Amount);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency, DecimalPlaces);
        }

        /// <summary>
        /// Check money equality
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>True for equality</returns>
        public static bool operator ==(Money left, Money right)
        {
            return EqualityComparer<Money>.Default.Equals(left, right);
        }

        /// <summary>
        /// Check money non equality
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>True for non equality</returns>
        public static bool operator !=(Money left, Money right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Allocate money in parts equally distributed. When it is impossible,
        /// the residual is addedto the last element.
        /// </summary>
        /// <param name="parts"></param>
        /// <returns>A list od Moneys equally distributed</returns>
        public IList<Money> Allocate(int parts)
        {
            if (parts < 1)
               throw new AllocateMoneyException(parts);

            var partEquallyDivided = Money.Create(this.Amount * (1m / parts), this.Currency);
            var residual = this - parts * partEquallyDivided;

            List<Money> result = new();
            for (int i = 0; i < parts; i++)
            {
                result.Add(partEquallyDivided);
                if (i == parts - 1)
                    result[i] += residual;
            }

            return result;
        }

        public IList<Money> Allocate(IList<Percent> listOfPercentage)
        {
            var sum = Percent.FromValue(listOfPercentage.Sum(p => p.Percentage));
            var residual = Percent.FromValue(100) - sum;
            listOfPercentage[listOfPercentage.Count - 1] += residual;

            return listOfPercentage.Select(p => this * p).ToList();
        }
    }
}
