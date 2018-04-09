using System.Collections.Generic;

namespace FinpeApi.Overviews
{
    public class OverviewDto
    {
        public decimal TotalIncome { get; set; }
        public List<ExpenseDto> Expenses { get; set; }
        public List<StatementDto> PendingStatements { get; set; }
        public List<string> Categories { get; set; }
        public decimal BankAmount { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
    }
}
