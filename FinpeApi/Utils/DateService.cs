using FinpeApi.ValueObjects;
using System;

namespace FinpeApi.Utils
{
    public class DateService : IDateService
    {
        public DateTime GetCurrentDateTime() => DateTime.UtcNow;

        public MonthYear GetCurrentMonthYear() => MonthYear.Create(DateTime.Today.Year, DateTime.Today.Month);
    }
}
