using System.Collections.Generic;

namespace FinpeApi.Overviews
{
    public class OverviewDto
    {
        public decimal TotalIncome { get; set; }
        public IReadOnlyList<ExpenseDto> Expenses { get; set; }
        public IReadOnlyList<StatementDto> PendingStatements { get; set; }
        public IReadOnlyList<string> Categories { get; set; }
        public decimal BankAmount { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
    }
}
