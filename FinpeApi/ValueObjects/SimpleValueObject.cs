namespace FinpeApi.ValueObjects
{
    public interface IValueObject<T>
    {
        T Value { get; }
    }

    public abstract class SimpleValueObject<T, VO> : IValueObject<T>
        where VO : IValueObject<T>
    {
        public T Value { get; protected set; }

        protected SimpleValueObject()
        { }

        protected SimpleValueObject(T value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            VO other = (VO)obj;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
