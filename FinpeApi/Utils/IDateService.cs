using System;

namespace FinpeApi.Utils
{
    public interface IDateService
    {
        MonthYear GetCurrentMonthYear();
        DateTime GetCurrentDateTime();
    }
}
