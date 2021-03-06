using System;

namespace FinpeApi.ValueObjects
{
    public class EntityId : SimpleValueObject<int, EntityId>
    {
        public static EntityId Create(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Must supply a valid id", nameof(value));

            return new EntityId(value);
        }

        protected EntityId(int value) : base(value) { }
    }
}
