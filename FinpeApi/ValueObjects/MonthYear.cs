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
    }
}
