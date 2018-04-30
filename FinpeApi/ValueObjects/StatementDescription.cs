using System;

namespace FinpeApi.ValueObjects
{
    public class StatementDescription : SimpleValueObject<string, StatementDescription>
    {
        public static StatementDescription Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Must supply a valid description", nameof(value));

            return new StatementDescription(value);
        }

        protected StatementDescription(string value) : base(value) { }

        public static implicit operator string(StatementDescription value)
        {
            return value.Value;
        }

        public static implicit operator StatementDescription(string value)
        {
            return Create(value);
        }
    }
}
