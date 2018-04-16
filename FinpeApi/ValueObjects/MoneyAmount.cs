using System;

namespace FinpeApi.ValueObjects
{
    public class MoneyAmount : SimpleValueObject<decimal, MoneyAmount>
    {
        public static MoneyAmount Create(decimal value)
        {
            if (value % 0.01m != 0)
                throw new ArgumentException("Must supply an amount with max precision of 2 decimals", "value");

            return new MoneyAmount(value);
        }

        protected MoneyAmount(decimal value) : base(value) { }

        public static implicit operator decimal(MoneyAmount value)
        {
            return value.Value;
        }

        public static implicit operator MoneyAmount(decimal value)
        {
            return Create(value);
        }
    }
}
