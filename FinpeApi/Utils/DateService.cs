using System;

namespace FinpeApi.Utils
{
    public class DateService : IDateService
    {
        public MonthYear GetCurrentMonthYear() => MonthYear.Create(DateTime.Today.Year, DateTime.Today.Month);
    }
}
