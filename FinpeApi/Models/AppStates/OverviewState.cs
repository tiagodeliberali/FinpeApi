using System.Collections.Generic;

namespace FinpeApi.Models.AppStates
{
    public class OverviewState
    {
        public decimal TotalIncome { get; set; }
        public List<OverviewExpense> Expenses { get; set; }
        public List<OverviewStatement> PendingStatements { get; set; }
        public List<string> Categories { get; set; }
        public decimal BankAmount { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
    }
}
