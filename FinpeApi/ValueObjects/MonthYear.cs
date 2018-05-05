using System;
using System.Globalization;

namespace FinpeApi.ValueObjects
{
    public class MonthYear
    {
        public int Month { get; private set; }
        public int Year { get; private set; }

        private MonthYear() { }

        public static MonthYear Create(int year, int month)
        {
            if (year < 2000 || year > 2100)
                throw new ArgumentException("Year outside a valid range (2000-2100)", nameof(year));

            if (month < 1 || month > 12)
                throw new ArgumentException("Month outside a valid range (1-12)", nameof(month));

            return new MonthYear()
            {
                Month = month,
                Year = year
            };
        }

        public string GetMonthName() => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);

        public DateTime GetFirstDay()
        {
            return new DateTime(Year, Month, 1);
        }

        public DateTime GetDate(int day)
        {
            return new DateTime(Year, Month, day);
        }

        public MonthYear GetNextMonthYear()
        {
            DateTime nextMonthDate = GetFirstDay().AddMonths(1);
            return Create(nextMonthDate.Year, nextMonthDate.Month);
        }

        public static bool operator ==(MonthYear a, MonthYear b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(MonthYear a, MonthYear b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var valueObject = obj as MonthYear;

            if (valueObject is null)
                return false;

            return valueObject.Year == Year
                && valueObject.Month == Month;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Month.GetHashCode();
            hash = (hash * 7) + Year.GetHashCode();
            return hash;
        }
    }
}
