using System;
using System.Collections.Generic;
using System.Linq;
using FinpeApi.Categories;
using FinpeApi.Statements;
using FinpeApi.ValueObjects;

namespace FinpeApi.Overviews
{
    public class OverviewDto
    {
        private MonthYear monthYear;
        private MonthSummary summary;
        private IReadOnlyList<Category> categories;

        public decimal TotalIncome { get; private set; }
        public IReadOnlyList<ExpenseDto> Expenses { get; private set; }
        public IReadOnlyList<StatementDto> PendingStatements { get; private set; }
        public IReadOnlyList<string> Categories { get; private set; }
        public decimal BankAmount { get; private set; }
        public decimal InitialAmountBalance { get; private set; }
        public string MonthName { get; private set; }
        public int Year { get; private set; }

        public static OverviewDto Create(MonthYear monthYear, MonthSummary summary, IReadOnlyList<Category>  categories) 
            => new OverviewDto(monthYear, summary, categories);

        private OverviewDto(MonthYear monthYear, MonthSummary summary, IReadOnlyList<Category> categories)
        {
            this.monthYear = monthYear ?? throw new ArgumentNullException(nameof(monthYear));
            this.summary = summary ?? throw new ArgumentNullException(nameof(summary));
            this.categories = categories ?? throw new ArgumentNullException(nameof(categories));

            BuildOverview();
        }

        private void BuildOverview()
        {
            MonthName = monthYear.GetMonthName();
            Year = monthYear.Year;
            TotalIncome = summary.GetTotalIncome();                
            Expenses = FormatExpenses();
            PendingStatements = FormatPendingStatements();
            BankAmount = summary.GetCurrentBalance();
            InitialAmountBalance = summary.GetInitialBalance();
            Categories = GetCategories();
        }

        private IReadOnlyList<StatementDto> FormatPendingStatements() => summary
            .GetPendingExpenses()
            .Select(x => new StatementDto(x.Id, x.DueDate, x.Description, x.Amount, x.Category.Name))
            .ToList();

        private IReadOnlyList<ExpenseDto> FormatExpenses() => summary
            .GetExpenses()
            .GroupBy(x => x.Category)
            .Select(x => new ExpenseDto(x.Key, x.Sum(values => values.Amount.Value)))
            .ToList();

        private IReadOnlyList<string> GetCategories() => categories
            .Select(x => x.Name)
            .ToList();
    }
}
